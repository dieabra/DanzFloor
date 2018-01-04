using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.Enum
{
    public enum EstadoTicket
    {
        Abierto = 0,
        Cerrado
    }

    public static class EstadoTicketHelper
    {
        public static Dictionary<int, string> Obtener()
        {
            return new Dictionary<int, string>()
            {
                { (int)EstadoTicket.Abierto, EstadoTicket.Abierto.ToString() },
                { (int)EstadoTicket.Cerrado, EstadoTicket.Cerrado.ToString() }
            };
        }

        public static string ObtenerDescripcion(int id)
        {
            if (id == (int)EstadoTicket.Abierto)
                return EstadoTicket.Abierto.ToString();

            if(id == (int)EstadoTicket.Cerrado)
                return EstadoTicket.Cerrado.ToString();

            return EstadoTicket.Abierto.ToString();
        }
    }
}