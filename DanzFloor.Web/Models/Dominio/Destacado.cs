using DanzFloor.Web.Datos;
using DanzFloor.Web.Models.Dominio;
using DanzFloor.Web.Models.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DanzFloor.Web.Models
{
    public class Destacado: EntidadConArchivo
    {
        public Destacado()
        {
        //    TagsClienteVisualiza = new List<TagCliente>();
        //    TagsClienteNoVisualiza = new List<TagCliente>();
        }

        public Destacado(DestacadoViewModel viewModel, ApplicationDbContext db)
        {
            //TagsClienteVisualiza = new List<TagCliente>();
            //TagsClienteNoVisualiza = new List<TagCliente>();

            Modificar(viewModel, db);
        }

        public void Modificar(DestacadoViewModel viewModel, ApplicationDbContext db)
        {
            //var tagClienteContexto = new Repositorio<TagCliente>(db);

            ModificarArchivos(viewModel.ArchivosId, db);
            Nombre = viewModel.Nombre;
            Titulo = viewModel.Titulo;
            Descripcion = viewModel.Descripcion;
            FechaPublicacion = viewModel.FechaPublicacion.CustomParse();
            Habilitado = viewModel.Habilitado;
            Link = viewModel.Link;
            NombreLink = viewModel.NombreLink;

            // Actualizar tagsCliente visualiza

            //var tagsClienteVisualizaIdRemover = TagsClienteVisualiza.Select(x => x.Id).ToList();

            //foreach (var tagVisualizaId in tagsClienteVisualizaIdRemover)
            //    TagsClienteVisualiza.Remove(tagClienteContexto.Traer(tagVisualizaId));

            //if (viewModel.TagsClienteVisualizaId != null)
            //    foreach (var tagVisualizIdNuevo in viewModel.TagsClienteVisualizaId)
            //        TagsClienteVisualiza.Add(tagClienteContexto.Traer(tagVisualizIdNuevo));


            //// Actualizar tagsCliente No visualiza

            //var tagsClienteNoVisualizaIdRemover = TagsClienteNoVisualiza.Select(x => x.Id).ToList();

            //foreach (var tagNoVisualizaId in tagsClienteNoVisualizaIdRemover)
            //    TagsClienteNoVisualiza.Remove(tagClienteContexto.Traer(tagNoVisualizaId));

            //if(viewModel.TagsClienteNoVisualizaId != null)
            //    foreach (var tagNoVisualizaIdNuevo in viewModel.TagsClienteNoVisualizaId)
            //        TagsClienteNoVisualiza.Add(tagClienteContexto.Traer(tagNoVisualizaIdNuevo));
        }

        public static Destacado ObtenerDestacado(List<Guid> clienteTagsClienteId, ApplicationDbContext db)
        {
            var resultado = new Repositorio<Destacado>(db).TraerTodos()
                .Where(x => x.FechaPublicacion < DateTime.Now &&
                    x.Habilitado)
                .OrderBy(x => x.Id);

            // Filtra Destacado según tagsCliente
            var filtrado = resultado;
            //.Where(x =>
            //    (x.TagsClienteNoVisualiza.Any(y => !clienteTagsClienteId.Contains(y.Id)) ||
            //        !x.TagsClienteNoVisualiza.Any()) &&
            //    (x.TagsClienteVisualiza.Any(y => clienteTagsClienteId.Contains(y.Id)) ||
            //        !x.TagsClienteVisualiza.Any())
            //);

            if (!filtrado.Any())
                return null;

            Random random = new Random();

            int randomNumber = random.Next(0, filtrado.Count());

            return filtrado.Skip(randomNumber).Take(1).First();
        }

        [Display(Name = "Titulo")]
        public string Titulo { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Fecha de publicación")]
        [Required(ErrorMessage = "Debe ingresar una fecha de publicación.")]
        public DateTime FechaPublicacion { get; set; }
        
        [Display(Name = "Habilitado")]
        public bool Habilitado { get; set; }

        [Display(Name = "Link")]
        public string Link { get; set; }

        [Display(Name = "Nombre del link")]
        public string NombreLink { get; set; }

        //[Display(Name = "Clientes que visualizan el destacado")]
        //public virtual ICollection<TagCliente> TagsClienteVisualiza { get; set; }

        //[Display(Name = "Clientes que no visualizan el destacado")]
        //public virtual ICollection<TagCliente> TagsClienteNoVisualiza { get; set; }
    }
}