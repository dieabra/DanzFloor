using DanzFloor.Web.Datos;
using DanzFloor.Web.Helpers;
using DanzFloor.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DanzFloor.Web.Models
{
    public abstract class EntidadConArchivo : Entidad
    {
        public EntidadConArchivo()
        {
            Archivos = new List<Archivo>();
        }

        [Display(Name = "Archivos")]
        public virtual ICollection<Archivo> Archivos { get; set; }

        public void ModificarArchivos(string ArchivosId, ApplicationDbContext db)
        {
            var archivoContexto = new Repositorio<Archivo>(db);
            var archivosId = Archivos.Select(x => x.Id);

            if (archivosId.Count() > 0)
                foreach (var id in archivosId.ToList())
                {
                    Archivos.Remove(archivoContexto.Traer(id));
                }

            if (!string.IsNullOrEmpty(ArchivosId))
                foreach (var archivoId in ArchivosId.Split(';'))
                {
                    if (!string.IsNullOrEmpty(archivoId))
                        Archivos.Add(archivoContexto.Traer(new Guid(archivoId)));
                }
        }
    }
}