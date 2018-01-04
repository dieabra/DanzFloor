using System;
using System.Globalization;

namespace System
{

    public static class StringExtension
    {
        public static DateTime CustomParse(this string entidad)
        {
            DateTime resultado = DateTime.MinValue;
            try
            {
                resultado = (string.IsNullOrEmpty(entidad)) ? DateTime.MinValue : DateTime.Parse(entidad);
            }
            catch
            {
                resultado = DateTime.Parse(entidad.Substring(3, 2) + "/" + entidad.Substring(0, 2) + "/" + entidad.Substring(6, 4));
            }

            return resultado;
        }

        public static DateTime? CustomParseNullable(this string entidad)
        {
            if (entidad == null)
                return null;
            
            return entidad.CustomParse();
        }

        public static string ObtenerIdFactura(this string entidad)
        {
            if (entidad == null)
                return null;
            
            var resultado = entidad.Split(' ');

            return resultado[3];
        }

    }
}