using DanzFloor.Web.Datos;
using DanzFloor.Web.Models.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DanzFloor.Web.Models.ViewModels
{
    public abstract class EntidadConArchivoViewModel : EntidadViewModel
    {
        public EntidadConArchivoViewModel()
        {
            Archivos = new List<Archivo>();
        }

        internal void InicializarComponenteArchivo(TipoEntidad entidad, ApplicationDbContext db)
        {
            FileComponentViewModel = new FileComponentViewModel
            {
                Configuraciones = new Repositorio<ArchivoConfiguracion>(db).TraerTodos()
                .Where(x => x.TipoEntidad == entidad).ToList(),

                //TagsCliente = new Repositorio<TagCliente>(db).TraerTodos().ToList()
            };
        }   

        internal void CargarComponenteArchivo(EntidadConArchivo entidad)
        {
            Archivos = entidad.Archivos.ToList();
            ArchivosId = string.Join(";", Archivos.Select(x => x.Id.ToString()));
            FileComponentViewModel.Archivos = Archivos.ToList();
        }

        public List<Archivo> Archivos { get; set; }

        public string ArchivosId { get; set; }

        public FileComponentViewModel FileComponentViewModel { get; set; }
    }
}