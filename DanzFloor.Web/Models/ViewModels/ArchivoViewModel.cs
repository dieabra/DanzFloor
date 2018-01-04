using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.ViewModels
{
    public class ArchivoViewModel
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public string Contenido { get; set; }
    }
}