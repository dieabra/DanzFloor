using System;
using System.Globalization;

namespace System
{

    public static class DateTimeExtension
    {
        public static string DiferenciaActual(this DateTime entidad)
        {
            var span = new TimeSpan();
            span = DateTime.Now - entidad;

            var dias = span.TotalDays.ToString();
            var horas = span.TotalHours.ToString();
            var minutes = span.TotalMinutes.ToString();

            if (span.TotalDays >= 1 && span.TotalDays < 2)
                return "Hace " + Math.Floor(span.TotalDays).ToString() + " día";
            else if (span.TotalDays > 1 && span.TotalDays <= 16)
                return "Hace " + Math.Floor(span.TotalDays).ToString() + " días";
            else if (span.TotalHours >= 1 && span.TotalHours < 2)
                return "Hace " + Math.Floor(span.TotalHours).ToString() + " hora";
            else if (span.TotalHours > 1 && span.TotalDays <= 16)
                return "Hace " + Math.Floor(span.TotalHours).ToString() + " horas";
            else if (span.TotalMinutes >= 5 && span.TotalDays <= 16)
                return "Hace " + Math.Floor(span.TotalMinutes).ToString() + " minutos";
            else if (span.TotalMinutes < 5)
                return " segundos";


            if (span.TotalDays > 365)
                return entidad.ToString("dd", new CultureInfo("es-AR"))
                        + " de " + entidad.ToString("MMMM", new CultureInfo("es-AR"))
                        + " de " + entidad.ToString("yyyy", new CultureInfo("es-AR"));
            else
                return entidad.ToString("dd", CultureInfo.CurrentCulture)
                        + " de " + entidad.ToString("MMMM", new CultureInfo("es-AR"));
        }

        public static DateTime DiferenciaActual(this string entidad)
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
    }
}