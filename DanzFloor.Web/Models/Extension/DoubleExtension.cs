using System;
using System.Globalization;

namespace System
{

    public static class DoubleExtension
    {
        private static CultureInfo culturaEsAr = new CultureInfo("es-AR");
        private static CultureInfo culturaEnUs = new CultureInfo("en-US");

        public static string ObtenerDecimales(this double entidad, int decimales = 2)
        {
            return Math.Round(entidad - Math.Truncate(entidad), decimales)
                .ToString(culturaEsAr).Replace("0,", "").PadRight(2, '0');
        }

        public static string VistaPrecio(this double entidad, int cantidad = 2)
        {
            var resultado = String.Format(culturaEsAr, "{0:0,0.00}", Math.Round(entidad, cantidad));
            var array = resultado.Split(',');

            if (array[0].Length == 2 && array[0].Substring(0, 1) == "0")
                array[0] = array[0].Substring(1, 1);

            return string.Join(",", array);
        }

        public static string VistaPrecioBejerman(this double entidad, int cantidad = 2)
        {
            return String.Format(culturaEnUs, "{0:0.00}", Math.Round(entidad, cantidad));
            //Math.Round(entidad, cantidad).ToString(CultureInfo.CreateSpecificCulture("en-US"));
        }

        public static double AplicarPorcentual(this double entidad, double porcentual)
        {
            var resultado = (porcentual * entidad) / 100;

            return entidad + resultado;
        }
    }
}