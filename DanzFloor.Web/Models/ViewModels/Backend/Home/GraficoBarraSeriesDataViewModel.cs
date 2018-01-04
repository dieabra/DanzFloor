using System;
using System.Globalization;

namespace DanzFloor.Web.Models.ViewModels.Backend.Home
{
    public class GraficoBarraSeriesDataViewModel
    {
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

        public decimal y { get; set; }

        private string _drilldown;
        public string drilldown
        {
            get
            {
                return _drilldown;
            }

            set
            {
                int numeroMes = 0;
                if (!Int32.TryParse(value, out numeroMes))
                    _drilldown = value;
                else
                    _drilldown = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(numeroMes);
            }
        }
    }
}