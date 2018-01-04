using DanzFloor.Web.Models.Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DanzFloor.Web.Models.ViewModels;
using DanzFloor.Web.Datos;

namespace DanzFloor.Web.Models
{
    public class GrupoTag : Entidad
    {
        public GrupoTag()
        {
            Tags = new List<Tag>();
        }

        public GrupoTag(GrupoTagViewModel viewModel, ApplicationDbContext db)
        {
            Tags = new List<Tag>();
            Modificar(viewModel, db);
        }

        internal void Modificar(GrupoTagViewModel viewModel, ApplicationDbContext db)
        {
            var tagContexto = new Repositorio<Tag>(db);

            Nombre = viewModel.Nombre;
            VisibleFront = viewModel.VisibleFront;
            EsCaracteristica = viewModel.EsCaracteristica;

            var tagsIdRemover = Tags.Select(x => x.Id).ToList();

            foreach (var tagId in tagsIdRemover)
                Tags.Remove(tagContexto.Traer(tagId));

            foreach (var tagIdNuevo in viewModel.TagsId)
                Tags.Add(tagContexto.Traer(tagIdNuevo));
        }

        [Display(Name = "Tags")]
        public virtual ICollection<Tag> Tags { get; set; }

        [Display(Name = "Visible en frontend")]
        public bool VisibleFront { get; set; }

        [Display(Name = "Es Característica")]
        public bool EsCaracteristica { get; set; }
    }
}