using DanzFloor.Web.Models.Dominio.Usuarios;
using DanzFloor.Web.Models.ViewModels;
using System.Collections.Generic;

namespace DanzFloor.Web.Models.Dominio
{
    public class ArtistaBase : EntidadConArchivo
    {
        public ArtistaBase()
        {
            Presentaciones = new List<Presentacion>();
            Temas = new List<Tema>();
        }

        public ArtistaBase(ArtistaViewModel viewModel, ApplicationDbContext db)
        {
            Modificar(viewModel, db);
        }

        internal void Modificar(ArtistaViewModel viewModel, ApplicationDbContext db)
        {
            ModificarArchivos(viewModel.ArchivosId, db);

            Nombre = viewModel.Nombre;            
        }

        public virtual ICollection<Presentacion> Presentaciones { get; set; }

        public virtual ICollection<Tema> Temas { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}