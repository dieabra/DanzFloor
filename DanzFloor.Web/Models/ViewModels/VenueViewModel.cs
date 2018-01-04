using DanzFloor.Web.Datos;
using DanzFloor.Web.Models.Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.ViewModels
{
    public class VenueViewModel: EntidadConArchivoViewModel
    {
        public VenueViewModel()
        {
            Escenarios = new List<Escenario>();
        }

        public VenueViewModel(ApplicationDbContext db)
        {
            InicializarComponenteArchivo(TipoEntidad.venue, db);
            Escenarios = new Repositorio<Escenario>(db).TraerTodos().OrderBy(x => x.Nombre).ToList();
        }

        public VenueViewModel(Venue modelo, ApplicationDbContext db)
        {
            InicializarComponenteArchivo(TipoEntidad.venue, db);
            Escenarios = new Repositorio<Escenario>(db).TraerTodos().OrderBy(x => x.Nombre).ToList();

            Id = modelo.Id;
            Nombre = modelo.Nombre;
            Direccion = modelo.Direccion;
            Localidad = modelo.Localidad;
            Latitud = modelo.Latitud;
            Longitud = modelo.Longitud;
            Habilitado = modelo.Habilitado;
            EscenariosId = modelo.Escenarios.Select(x => x.Id).ToList();
        }

        internal void RegenerarVista(ApplicationDbContext db)
        {
            InicializarComponenteArchivo(TipoEntidad.venue, db);
            var venue = new Repositorio<Venue>(db).Traer(Id);

            if (venue != null)
            {
                CargarComponenteArchivo(venue);
            }

            Escenarios = new Repositorio<Escenario>(db).TraerTodos().OrderBy(x => x.Nombre).ToList();
        }

        public List<Escenario> Escenarios { get; set; }
        
        public List<Guid> EscenariosId { get; set; }

        [Display(Name = "Direccion")]
        public string Direccion { get; set; }

        [Display(Name = "Localidad")]
        public string Localidad { get; set; }

        [Display(Name = "Latitud")]
        public string Latitud { get; set; }

        [Display(Name = "Longitud")]
        public string Longitud { get; set; }

        [Display(Name = "Habilitado")]
        public bool Habilitado { get; set; }
    }
}