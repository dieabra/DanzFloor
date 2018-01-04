using DanzFloor.Web.Datos;
using DanzFloor.Web.Models.Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.ViewModels
{
    public class GrupoTagViewModel: EntidadViewModel
    {
        public GrupoTagViewModel()
        {
            Tags = new List<Tag>();
        }

        public GrupoTagViewModel(ApplicationDbContext db, bool caracteristica)
        {
            Tags = new Repositorio<Tag>(db).TraerTodos().OrderBy(x => x.Nombre).ToList();
            EsCaracteristica = caracteristica;
        }

        public GrupoTagViewModel(GrupoTag modelo, ApplicationDbContext db)
        {
            Tags = new Repositorio<Tag>(db).TraerTodos().OrderBy(x => x.Nombre).ToList();

            Id = modelo.Id;
            Nombre = modelo.Nombre;
            TagsId = modelo.Tags.Select(x => x.Id).ToList();
            VisibleFront = modelo.VisibleFront;
            EsCaracteristica = modelo.EsCaracteristica;
        }

        internal void RegenerarVista(ApplicationDbContext db)
        {
            Tags = new Repositorio<Tag>(db).TraerTodos().OrderBy(x => x.Nombre).ToList();
        }


        public List<Tag> Tags { get; set; }

        [Required(ErrorMessage = "Debe seleccionar al menos un tag para agrupar.")]
        public List<Guid> TagsId { get; set; }

        public bool VisibleFront { get; set; }

        [Display(Name = "Es Característica")]
        public bool EsCaracteristica { get; set; }
    }
}