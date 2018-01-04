using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.ViewModels
{
    public class PaginadoViewModel 
    {
        internal void BusquedaSinResultados<T>() 
        {
            Resultado = new List<T>();
            Pagina = 0;
            Total = 0;
            TotalPaginas = 0;
        }

        public int Pagina { get; set; }

        public int TotalPaginas { get; set; }

        public int Total { get; set; }

        public string AccionSubmit { get; set; }

        public object Resultado { get; set; }

        public string Redirect { get; set; }
    }
}