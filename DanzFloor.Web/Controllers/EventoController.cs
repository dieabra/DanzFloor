using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DanzFloor.Web.Models;
using DanzFloor.Web.Models.Dominio;
using DanzFloor.Web.Datos;
using DanzFloor.Web.Models.Enum;
using DanzFloor.Web.Models.ViewModels.Backend;

namespace DanzFloor.Web.Controllers
{
    public class EventoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Index()
        {
            return View(new Repositorio<Evento>(db).TraerTodos());
        }
        
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Create()
        {
            var viewModel = new EventoViewModel(db);

            return View(viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Create(EventoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var eventoCrear = new Evento(viewModel, db);
                new Repositorio<Evento>(db).Crear(eventoCrear);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = new Repositorio<Evento>(db).Traer(id.Value);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Edit([Bind(Include = "Id,Nombre,FechaCreacion,FechaEdicion,Eliminado,Habilitado")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                new Repositorio<Evento>(db).Modificar(evento);
                return RedirectToAction("Index");
            }
            return View(evento);
        }

        [HttpGet]
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Delete(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    return null;// new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Evento evento = new Repositorio<Evento>(db).Traer(id.Value);
                if (evento == null)
                    return null;
                else
                    new Repositorio<Evento>(db).Eliminar(evento);

                return Json(new
                {
                    Resultado = Resultado.OK,
                    Mensaje = ""
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Resultado = Resultado.Error,
                    Mensaje = ""
                }, JsonRequestBehavior.AllowGet);
            }
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Evento evento = new Repositorio<Evento>(db).Traer(id);
            new Repositorio<Evento>(db).Eliminar(evento);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
