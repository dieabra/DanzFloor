using DanzFloor.Web.Datos;
using DanzFloor.Web.Models.Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.ViewModels.Backend
{
    public class EventoViewModel : EntidadConArchivoViewModel
    {
        public EventoViewModel()
        {
            InicializarEntidad();
        }

        public EventoViewModel(ApplicationDbContext db)
        {
            InicializarEntidad();
            Venues = new Repositorio<Venue>(db).TraerTodos();
        }

        private void InicializarEntidad()
        {
            Fechas = new List<Fecha>();
            Venues = new List<Venue>();
        }

        [Required(ErrorMessage = "Debe seleccionar al menos una Fecha")]
        public List<Guid> FechasId { get; set; }

        [RequiredGuid(ErrorMessage = "Debe seleccionar un Venue")]
        public Guid VenueId { get; set; }

        public IEnumerable<Fecha> Fechas { get; set; }

        public IEnumerable<Venue> Venues { get; set; }
    }
}