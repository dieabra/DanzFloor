using DanzFloor.Web.Datos;
using DanzFloor.Web.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DanzFloor.Web.Models.Dominio
{
    public class Venue : EntidadConArchivo
    {
        public Venue()
        {
            Escenarios = new List<Escenario>();
        }

        public Venue(VenueViewModel viewModel, ApplicationDbContext db)
        {
            Escenarios = new List<Escenario>();
            Modificar(viewModel, db);
        }

        internal void Modificar(VenueViewModel viewModel, ApplicationDbContext db)
        {
            var escenarioContexto = new Repositorio<Escenario>(db);

            ModificarArchivos(viewModel.ArchivosId, db);

            Nombre = viewModel.Nombre;
            Direccion = viewModel.Direccion;
            Localidad = viewModel.Localidad;
            Latitud = viewModel.Latitud;
            Longitud = viewModel.Longitud;
            Habilitado = viewModel.Habilitado;

            var escenariosIdRemover = Escenarios.Select(x => x.Id).ToList();

            foreach (var escenarioId in escenariosIdRemover)
                Escenarios.Remove(escenarioContexto.Traer(escenarioId));
            
            if(viewModel.EscenariosId != null)
                foreach (var escenarioIdNuevo in viewModel.EscenariosId)
                    Escenarios.Add(escenarioContexto.Traer(escenarioIdNuevo));
        }

        [Display(Name = "Escenarios")]
        public virtual ICollection<Escenario> Escenarios { get; set; }
        
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