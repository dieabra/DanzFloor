using DanzFloor.Web.Models.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.ViewModels
{
    public class FileComponentViewModel
    {
        public FileComponentViewModel()
        {
            Archivos = new List<Archivo>();
            Configuraciones = new List<ArchivoConfiguracion>();
            //TagsCliente = new List<TagCliente>();
        }

        public List<Archivo> Archivos { get; set; }

        public List<ArchivoConfiguracion> Configuraciones { get; set; }

        //public List<TagCliente> TagsCliente { get; set; }
    }
}