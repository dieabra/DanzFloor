using DanzFloor.Web.Models.Dominio.FeedBack;
using System;
using System.Collections.Generic;

namespace DanzFloor.Web.Models.Dominio
{
    public class Fecha: EntidadConArchivo
    {
        public DateTime Dia { get; set; }
        public virtual List<LineUp> LineUps { get; set; }
    }
}