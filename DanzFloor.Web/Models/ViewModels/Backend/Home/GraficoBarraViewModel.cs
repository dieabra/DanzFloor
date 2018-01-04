using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.ViewModels.Backend.Home
{
    public class GraficoBarraViewModel
    {
        public GraficoBarraViewModel()
        {
            series = new List<GraficoBarraSeriesViewModel>();
        }

        public List<GraficoBarraSeriesViewModel> series { get; set; }

        public GraficoBarraDrilldownViewModel drilldown { get; set; }
    }
}