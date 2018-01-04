using DanzFloor.Web.Models.Dominio.Usuarios;
using DanzFloor.Web.Models.ViewModels;
using System.Collections.Generic;

namespace DanzFloor.Web.Models.Dominio
{
    public class Artista: ArtistaBase
    {
        public Artista() : base()
        {
            Bandas = new List<Banda>();
        }

        public Artista(ArtistaViewModel viewModel, ApplicationDbContext db) : base(viewModel, db)
        {
            Bandas = new List<Banda>();
        }

        public virtual ICollection<Banda> Bandas { get; set; }
    }
}