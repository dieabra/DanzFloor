using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.LogEventos
{
    public class LoggerEventos
    {

        string path;
        public LoggerEventos(string Path)
        {
            this.path = Path;
        }

        public void EscribirLog(string mensaje)
        {
            try
            {
                mensaje = " " + DateTime.Now.ToString() + " - " + mensaje + ".";
                Console.WriteLine(mensaje);
                var path = this.path;
                CreatePathIfNotExist(path);
                System.IO.StreamWriter file = new System.IO.StreamWriter(path, true);
                file.WriteLine(mensaje);
                file.Close();
            }
            catch (Exception)
            {
            }
        }

        internal void EscribirError(Exception ex)
        {
            var mensaje = " " + DateTime.Now.ToString() + " - " + ex.Message + ".";
            if (ex.InnerException != null)
                mensaje = mensaje + " " + ex.InnerException.Message + ".";

            mensaje = mensaje + ex.StackTrace;

            Console.WriteLine(mensaje);
            var path = this.path;

            CreatePathIfNotExist(path);
            System.IO.StreamWriter file = new System.IO.StreamWriter(path, true);
            file.WriteLine(mensaje);
            file.Close();
        }

        public string CreatePathIfNotExist(string path)
        {
            var folder = Path.GetDirectoryName(path);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            return path;
        }

    }
}