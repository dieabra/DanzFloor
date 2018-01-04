using DanzFloor.Web.Models.Dominio.FeedBack;
using System.Collections.Generic;

namespace DanzFloor.Web.Models.Dominio
{
    public class LineUp: EntidadConArchivo
    {
        public virtual List<Presentacion> Presentaciones { get; set; }
        public virtual Escenario Escenario { get; set; }
    }
}