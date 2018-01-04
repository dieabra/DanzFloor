using DanzFloor.Web.Models.Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models
{
    public class Archivo: Entidad
    {
        public Archivo()
        {
        }

        public static string ObtenerUrlArchivo(IEnumerable<Archivo> archivos, TipoEntidad tipoEntidadId, string configuracion, bool imagenDefault = true, string height = null, string width = null, string mode = null, string quality = null)
        {
            var archivo = archivos.FirstOrDefault(x => 
            x.ArchivoConfiguracion.TipoEntidad == tipoEntidadId &&
            x.ArchivoConfiguracion.Nombre == configuracion);


            var archivoConfig = "";
            if (height != null || width != null || mode != null || quality != null)
            {
                archivoConfig += "|";   
                if (height != null)
                    archivoConfig += "height-" + height + "|";
                if (width != null)
                    archivoConfig += "width-" + width + "|";
                if (mode != null)
                    archivoConfig += "mode-" + mode + "|";
                if (quality != null)
                    archivoConfig += "quality-" + quality + "|";                
            }

           

            if (archivo == null)
                return imagenDefault ? "/assets/SdP/Frontend/img/id-sapore-bw.jpg" : "";

            
            return ConfigurationManager.AppSettings["Core"] + "/Archivo/ObtenerImagenPorId/" + archivo.Id + archivoConfig;
        }

        public static List<string> ObtenerUrlArchivos(IEnumerable<Archivo> archivos, TipoEntidad tipoEntidadId, string configuracion, bool imagenDefault = true, IEnumerable<Guid> tagsClienteId = null)
        {
            var resultado = new List<string>();
            var entidades = archivos.Where(x =>
                x.ArchivoConfiguracion.TipoEntidad == tipoEntidadId &&
                x.ArchivoConfiguracion.Nombre == configuracion);

            if (!entidades.Any())
                return imagenDefault ? new List<string>() { "/assets/SdP/Frontend/img/id-sapore-bw.jpg" } : new List<string>();

            //if (entidades.First().ArchivoConfiguracion.HabilitaTagsCliente)
            //{
            //    entidades = entidades.Where(x => 
            //        (
            //            x.TagsCliente.Any() &&
            //            tagsClienteId != null &&
            //            x.TagsCliente.Any(y => tagsClienteId.Contains(y.Id))
            //        ) || 
            //        !x.TagsCliente.Any()
            //    );
            //}

            return entidades.Select(x =>
                ConfigurationManager.AppSettings["Core"] + "/Archivo/ObtenerImagenPorId/" + x.Id
            ).ToList();
        }

        public static string ObtenerUrlDescargaArchivo(IEnumerable<Archivo> archivos, TipoEntidad tipoEntidadId, string configuracion)
        {
            var archivo = archivos.FirstOrDefault(x =>
            x.ArchivoConfiguracion.TipoEntidad == tipoEntidadId &&
            x.ArchivoConfiguracion.Nombre == configuracion);

            if (archivo == null)
                return null;
            
            return ConfigurationManager.AppSettings["Core"] + "/Archivo/ObtenerArchivo/" + archivo.Id;
        }

        [Display(Name = "Título")]
        [MaxLength(200, ErrorMessage = "El título debe tener como máximo 200 caracteres")]
        public string Titulo { get; set; }

        [Display(Name = "Descripcion")]
        [MaxLength(500, ErrorMessage = "La descripcion debe tener como máximo 500 caracteres")]
        public string Descripcion { get; set; }

        [Display(Name = "Contenido")]
        public string Contenido { get; set; }

        [Display(Name = "Contiguración")]
        public virtual ArchivoConfiguracion ArchivoConfiguracion { get; set; }
        
    }
}