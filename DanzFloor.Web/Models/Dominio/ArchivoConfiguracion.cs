using System.Collections.Generic;

namespace DanzFloor.Web.Models
{
    public class ArchivoConfiguracion : Entidad
    {
        public TipoEntidad TipoEntidad { get; set; }
        
        public string Extenciones { get; set; }

        public bool Requerido { get; set; }

        public int TamanoMaximo { get; set; }

        public bool EsCargaMultiple { get; set; }

        public bool NoEditable { get; set; }

        public bool HabilitaTagsCliente { get; set; }
    }
}