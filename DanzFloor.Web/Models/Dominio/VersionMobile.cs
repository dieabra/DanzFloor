using DanzFloor.Web.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanzFloor.Web.Models
{
    public class VersionMobile : Entidad
    {
        [DisplayName("Version Minima")]
        [Required(ErrorMessage = "Ingrese la version minima. Ejemplo: 50.1.0")]
        //[RegularExpression("^[01]?[.]?\\(?[2-9]\\d{2}\\)?[.]?\\d{3}$",
        //ErrorMessage = "Phone is required and must be properly formatted.")]
        public String VersionMinima { get; set; }

        [DisplayName("Version Actual")]
        [Required(ErrorMessage = "Ingrese la version actual. Ejemplo: 1.1.9")]
        [VersionMobileValidation(ErrorMessage = "La versión actual debe ser superior a la minima")]
        public String VersionActual { get; set; }


        public bool VersionValida(string version)
        {
            bool versionValida = false;

            if (!String.IsNullOrEmpty(version))
            {
                //Parseo la version recibida en 3 numeros por separados que son los que voy a comparar
                int versionP = 0, versionMin = 0, versionAct = 0;
                Int32.TryParse(version.Replace(".", ""), out versionP);
                Int32.TryParse(this.VersionMinima.Replace(".", ""), out versionMin);
                Int32.TryParse(this.VersionActual.Replace(".", ""), out versionAct);

                if (versionMin <= versionP && versionP <= versionAct)
                {
                    versionValida = true;
                }
            }

            return versionValida;
        }

    }
}
