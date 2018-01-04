using DanzFloor.Web.Datos;
using DanzFloor.Web.Models.Dominio;

namespace DanzFloor.Web.Models.ViewModels
{
    public class ArtistaViewModel: EntidadConArchivoViewModel
    {
        public ArtistaViewModel()
        {

        }

        public ArtistaViewModel(ApplicationDbContext db)
        {
            InicializarComponenteArchivo(TipoEntidad.artista, db);
        }

        public ArtistaViewModel(Artista modelo, ApplicationDbContext db)
        {
            InicializarComponenteArchivo(TipoEntidad.artista, db);

            Id = modelo.Id;
            Nombre = modelo.Nombre;
        }

        internal void RegenerarVista(ApplicationDbContext db)
        {
            InicializarComponenteArchivo(TipoEntidad.artista, db);
            var artista = new Repositorio<Artista>(db).Traer(Id);

            if (artista != null)
            {
                CargarComponenteArchivo(artista);
            }
            
        }
    }
}