using DanzFloor.Web.Models.ViewModels.Backend;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DanzFloor.Web.Models.Dominio
{
    public class Evento: EntidadConArchivo
    {
        public Evento()
        {
            Fechas = new List<Fecha>();
        }

        public Evento(EventoViewModel viewModel, ApplicationDbContext db)
        {
            Modificar(viewModel, db);
        }

        public void Modificar(EventoViewModel viewModel, ApplicationDbContext db)
        {
            Nombre = viewModel.Nombre;

        }

        [Required(ErrorMessage = "Debe seleccionar un tipo de evento")]
        public virtual TipoEvento TipoEvento { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un venue")]
        public virtual Venue Venue { get; set; }

        [Required(ErrorMessage = "Debe contener al menos una fecha")]
        public virtual ICollection<Fecha> Fechas { get; set; }
    }
}