using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.Enum
{
    public enum InstanciaPedido
    {
        Inicial,
        Pendiente,
        Facturado,
        GratisEnAprobacion,
        Rechazado,
        Procesando
    }

    public class InstanciaPedidoConst
    {
        public static List<InstanciaPedido> Instancias
        {
            get
            {
                List<InstanciaPedido> resultado = new List<InstanciaPedido>
                {
                    InstanciaPedido.Inicial,
                    InstanciaPedido.Pendiente,
                    InstanciaPedido.Procesando,
                    InstanciaPedido.Facturado,
                    InstanciaPedido.GratisEnAprobacion,
                    InstanciaPedido.Rechazado
                };

                return resultado;
            }
        }

        public static List<InstanciaPedido> InstanciasGeneradorasTicket
        {
            get
            {
                List<InstanciaPedido> resultado = new List<InstanciaPedido>
                {
                    InstanciaPedido.GratisEnAprobacion
                };

                return resultado;
            }
        }
    }
}