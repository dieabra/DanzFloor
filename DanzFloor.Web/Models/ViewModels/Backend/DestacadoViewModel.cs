using DanzFloor.Web.Datos;
using DanzFloor.Web.Models.Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.ViewModels
{
    public class DestacadoViewModel: EntidadConArchivoViewModel
    {
        public DestacadoViewModel()
        {
            //TagsCliente = new List<TagCliente>();
        }

        public DestacadoViewModel(ApplicationDbContext db)
        {
            //TagsCliente = new Repositorio<TagCliente>(db).TraerTodos();
            InicializarComponenteArchivo(TipoEntidad.destacado, db);
        }

        public DestacadoViewModel(Destacado model, ApplicationDbContext db)
        {
            //TagsCliente = new Repositorio<TagCliente>(db).TraerTodos();

            InicializarComponenteArchivo(TipoEntidad.destacado, db);
            CargarComponenteArchivo(model);
            Id = model.Id;
            Nombre = model.Nombre;
            Titulo = model.Titulo;
            Descripcion = model.Descripcion;
            FechaPublicacion = model.FechaPublicacion.ToShortDateString();
            Habilitado = model.Habilitado;
            Link = model.Link;
            NombreLink = model.NombreLink;
        }

        public void RegenerarVista(ApplicationDbContext db)
        {
            InicializarComponenteArchivo(TipoEntidad.destacado, db);
            var model = new Repositorio<Destacado>(db).Traer(Id);

            if (model != null)
                CargarComponenteArchivo(model);

            //TagsCliente = new Repositorio<TagCliente>(db).TraerTodos();
        }

        [Display(Name = "Titulo")]
        public string Titulo { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Fecha de publicación")]
        [Required(ErrorMessage = "Debe ingresar una fecha de publicación.")]
        public string FechaPublicacion { get; set; }

        [Display(Name = "Habilitado")]
        public bool Habilitado { get; set; }

        [Display(Name = "Link")]
        public string Link { get; set; }

        [Display(Name = "Nombre del link")]
        public string NombreLink { get; set; }

        //[Display(Name = "Tags Cliente")]
        //public IEnumerable<TagCliente> TagsCliente { get; set; }

        public List<Guid> TagsClienteVisualizaId { get; set; }

        public List<Guid> TagsClienteNoVisualizaId { get; set; }
    }
}