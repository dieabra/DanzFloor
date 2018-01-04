using DanzFloor.Web.Datos;
using DanzFloor.Web.Models.Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.ViewModels
{
    public class TemaViewModel: EntidadViewModel
    {
        public TemaViewModel()
        {
            Artistas = new List<Artista>();
        }

        public TemaViewModel(ApplicationDbContext db)
        {
            Artistas = new Repositorio<Artista>(db).TraerTodos().OrderBy(x => x.Nombre).ToList();
        }

        public TemaViewModel(Tema modelo, ApplicationDbContext db)
        {
            Artistas = new Repositorio<Artista>(db).TraerTodos().OrderBy(x => x.Nombre).ToList();

            Id = modelo.Id;
            Nombre = modelo.Nombre;
            SpotifyLink = modelo.SpotifyLink;
            ArtistaId = modelo.Artista.Id;
        }

        internal void RegenerarVista(ApplicationDbContext db)
        {
            Artistas = new Repositorio<Artista>(db).TraerTodos().OrderBy(x => x.Nombre).ToList();
        }

        public string SpotifyLink { get; set; }

        public List<Artista> Artistas { get; set; }

        [RequiredGuid(ErrorMessage = "Debe seleccionar un Artista")]
        public Guid ArtistaId { get; set; }
    }
}