using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.ViewModels.Backend.Home
{
    public class GraficoBarraSeriesDrilldownViewModel
    {
        public GraficoBarraSeriesDrilldownViewModel()
        {
            data = new List<GraficoBarraSeriesDrilldownDataViewModel>();
        }

        private string _name;
        public string name
        {
            get
            {
                return _name;
            }

            set
            {
                int numeroMes = 0;
                if (!Int32.TryParse(value, out numeroMes))
                    _name = value;
                else
                    _name = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(numeroMes);
            }
        }

        private string _id;
        public string id
        {
            get
            {
                return _id;
            }

            set
            {
                int numeroMes = 0;
                if (!Int32.TryParse(value, out numeroMes))
                    _id = value;
                else
                    _id = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(numeroMes);
            }
        }

        public List<GraficoBarraSeriesDrilldownDataViewModel> data { get; set; }
    }
}