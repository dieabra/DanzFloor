using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.DTO
{
    public class ArchivoEnvioDTO
    {
        public string Nombre { get; set; }

        public byte[] Bytes { get; set; }
    }
}