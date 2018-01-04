using DanzFloor.Web.Datos;
using DanzFloor.Web.Models.Dominio;
using DanzFloor.Web.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace DanzFloor.Web.Models
{
    public class Banda: Artista
    {
        public Banda() : base()
        {
            Artistas = new List<Artista>();
        }

        public Banda(BandaViewModel viewModel, ApplicationDbContext db) : base(viewModel, db)
        {
            Artistas = new List<Artista>();
            Modificar(viewModel, db);
        }

        internal void Modificar(BandaViewModel viewModel, ApplicationDbContext db)
        {
            var ArtistaContexto = new Repositorio<Artista>(db);

            var IdRemover = Artistas.Select(x => x.Id).ToList();

            foreach (var Id in IdRemover)
                Artistas.Remove(ArtistaContexto.Traer(Id));

            foreach (var IdNuevo in viewModel.ArtistasId)
                Artistas.Add(ArtistaContexto.Traer(IdNuevo));
        }

        public virtual ICollection<Artista> Artistas { get; set; }
    }
}