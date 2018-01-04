using System;
using System.Collections.Generic;
using DanzFloor.Web.Models.Dominio.FeedBack;

namespace DanzFloor.Web.Models.Dominio
{
    public class Presentacion: EntidadConArchivo
    {
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public virtual SetList SetList { get; set; }
        public virtual List<Artista> Artistas { get; set; }
        public virtual List<Artista> Invitados { get; set; }
    }
}