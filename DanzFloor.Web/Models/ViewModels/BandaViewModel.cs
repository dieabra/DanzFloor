using DanzFloor.Web.Datos;
using DanzFloor.Web.Models.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DanzFloor.Web.Models.ViewModels
{
    public class BandaViewModel: ArtistaViewModel
    {
        public BandaViewModel() : base()
        {
            Artistas = new List<Artista>();
        }

        public BandaViewModel(ApplicationDbContext db) : base (db)
        {
            Artistas = new Repositorio<Artista>(db).TraerTodos().OrderBy(x => x.Nombre).ToList();
        }

        public BandaViewModel(Banda modelo, ApplicationDbContext db) : base (modelo, db)
        {
            Artistas = new Repositorio<Artista>(db).TraerTodos().OrderBy(x => x.Nombre).ToList();
            Modificar(modelo, db);
        }

        public void Modificar(Banda modelo, ApplicationDbContext db)
        {
            ArtistasId = modelo.Artistas.Select(x => x.Id).ToList();
        }

        public List<Guid> ArtistasId { get; set; }

        public List<Artista> Artistas { get; set; }
    }
}