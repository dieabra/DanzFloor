using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.Enum
{
    public enum NivelRecomendado
    {
        //SinDestacar = 0,
        //Forzado = 1,
        //Automatico = 2
        SinRecomendar = 0,
        ForzarRecomendar = 1,
        ExcluirRecomendados = 2
    }

    public static class NivelRecomendadoConst
    {

        public static List<NivelRecomendado> Niveles
        {
            get
            {
                List<NivelRecomendado> resultado = new List<NivelRecomendado>
                {
                    //NivelRecomendado.SinDestacar,
                    //NivelRecomendado.Forzado,
                    //NivelRecomendado.Automatico,
                    NivelRecomendado.SinRecomendar,
                    NivelRecomendado.ForzarRecomendar,
                    NivelRecomendado.ExcluirRecomendados,
                };

                return resultado;
            }
        }

        public static string ObtenerDescripcion(NivelRecomendado nivel)
        {
            switch (nivel)
            {
                //case NivelRecomendado.SinDestacar:
                //    return "Sin recomendar";
                //case NivelRecomendado.Automatico:
                //    return "Automático";
                //case NivelRecomendado.Forzado:
                //    return "Forzado";
                case NivelRecomendado.SinRecomendar:
                    return "Sin Recomendar";
                case NivelRecomendado.ForzarRecomendar:
                    return "Forzar Recomendado";
                case NivelRecomendado.ExcluirRecomendados:
                    return "Excluir Recomendado";
            }
            
            return "Sin destacar";
        }
    }
}