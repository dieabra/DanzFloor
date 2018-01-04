using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.ViewModels.Backend.Home
{
    public class GraficoBarraSeriesViewModel
    {
        public GraficoBarraSeriesViewModel()
        {
            data = new List<GraficoBarraSeriesDataViewModel>();
        }

        public string name { get; set; }

        public bool colorByPoint { get; set; }

        public List<GraficoBarraSeriesDataViewModel> data { get; set; }
    }
}