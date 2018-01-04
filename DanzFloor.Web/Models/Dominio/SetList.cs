using System.Collections.Generic;

namespace DanzFloor.Web.Models.Dominio
{
    public class SetList:Entidad
    {
        /// <summary>
        /// Tendrian que estar ordenados
        /// </summary>
        public virtual List<Tema> Temas { get; set; }
    }
}