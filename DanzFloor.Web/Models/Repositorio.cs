using DanzFloor.Web.Models;
using DanzFloor.Web.Models.Datos;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.Validation;

namespace DanzFloor.Web.Datos
{
    public class Repositorio<T> : IRepositorio<T> where T : class, IEntidad
    {
        private readonly DbContext dbContext;
        public Repositorio(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual T Traer(Guid Id)
        {
            var entidad = this.dbContext.Set<T>().Find(Id);

            return entidad;
        }
        
        public IQueryable<T> TraerTodos(bool inclusiveEliminados = false)
        {
            return this.dbContext.Set<T>().Where(x => !x.Eliminado || inclusiveEliminados);
        }

        public virtual void Crear(T entidad, bool grabarCambios = true)
        {
            //Users
            //var userStore = new UserStore<ApplicationUser>(dbContext);
            //var userManager = new UserManager<ApplicationUser>(userStore);

            //entidad.Autor =             
            entidad.FechaCreacion = DateTime.Now;
            entidad.FechaEdicion = entidad.FechaCreacion;
            entidad.Eliminado = false;
            this.dbContext.Set<T>().Add(entidad);
            var errores = this.dbContext.GetValidationErrors().ToList();
            if (grabarCambios)
                try
                {
                    this.dbContext.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        var error = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(new Exception(error)));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            var errorMessage = string.Format("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                            Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(new Exception(errorMessage)));
                        }
                    }
                    throw new Exception(e.Message);
                }
                catch (Exception e)
                {
                    Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(e));
                    throw new Exception(e.Message);
                }
        }

        public virtual void Modificar(T entidad, bool grabarCambios = true)
        {
            entidad.FechaEdicion = DateTime.Now;
            this.dbContext.Entry(entidad).State = EntityState.Modified;
            var errores = this.dbContext.GetValidationErrors();
            if (grabarCambios)
                try
                {
                    this.dbContext.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        var error = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(new Exception(error)));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            var errorMessage = string.Format("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                            Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(new Exception(errorMessage)));
                        }
                    }
                    throw new Exception(e.Message);
                }
                catch (Exception e)
                {
                    Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(e));
                    throw new Exception(e.Message);
                }
        }

        public virtual void Eliminar(T entidad, bool grabarCambios = true)
        {
            entidad.FechaEdicion = DateTime.Now;
            entidad.Eliminado = true;
            this.dbContext.Entry(entidad).State = EntityState.Modified;
            if (grabarCambios)
                try
                {
                    this.dbContext.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        var error = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(new Exception(error)));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            var errorMessage = string.Format("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                            Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(new Exception(errorMessage)));
                        }
                    }
                    throw new Exception(e.Message);
                }
                catch (Exception e)
                {
                    Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(e));
                    throw new Exception(e.Message);
                }
        }
        public virtual void GuardarCambios(T entidad = null)
        {
            try
            {
                this.dbContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    var error = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(new Exception(error)));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        var errorMessage = string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                        Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(new Exception(errorMessage)));
                    }
                }
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(new Elmah.Error(e));
                throw new Exception(e.Message);
            }
        }


        public Pagina<T> TraerPagina(int numeroPagina, int elementosPagina, string criterioOrdenamiento, bool ascendente, string criterioBusqueda)
        {
            var query = this.dbContext.Set<T>().Select(x => x);
            return query.ToPage(numeroPagina, elementosPagina);
        }

    }
}
