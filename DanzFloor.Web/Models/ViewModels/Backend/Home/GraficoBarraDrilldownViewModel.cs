using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.ViewModels.Backend.Home
{
    public class GraficoBarraDrilldownViewModel
    {
        public GraficoBarraDrilldownViewModel()
        {
            series = new List<GraficoBarraSeriesDrilldownViewModel>();
        }

        public List<GraficoBarraSeriesDrilldownViewModel> series { get; set; }
    }
}