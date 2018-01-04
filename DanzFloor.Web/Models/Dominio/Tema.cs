using DanzFloor.Web.Datos;
using DanzFloor.Web.Models.ViewModels;

namespace DanzFloor.Web.Models.Dominio
{
    public class Tema:Entidad
    {
        public Tema()
        {
        }

        public Tema(TemaViewModel viewModel, ApplicationDbContext db)
        {
            Modificar(viewModel, db);
        }

        internal void Modificar(TemaViewModel viewModel, ApplicationDbContext db)
        {
            Nombre = viewModel.Nombre;
            SpotifyLink = viewModel.SpotifyLink;
            Artista = new Repositorio<ArtistaBase>(db).Traer(viewModel.ArtistaId);
        }

        public virtual ArtistaBase Artista { get; set; }

        public string SpotifyLink { get; set; }
    }
}