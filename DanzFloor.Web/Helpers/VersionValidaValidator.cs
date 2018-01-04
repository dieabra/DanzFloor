using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanzFloor.Web.Helpers
{
    public class VersionMobileValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool versionValida = false;
            //Parseo las versiones en 3 numeros por separados que son los que voy a comparar
            int versionMin = 0, versionAct = 0;
            Int32.TryParse(((DanzFloor.Web.Models.VersionMobile)(validationContext.ObjectInstance)).VersionMinima.Replace(".", ""), out versionMin);
            Int32.TryParse(((DanzFloor.Web.Models.VersionMobile)(validationContext.ObjectInstance)).VersionActual.Replace(".", ""), out versionAct);

            if (versionMin <= versionAct)
            {
                versionValida = true;
            }

            return versionValida ? ValidationResult.Success : new ValidationResult(this.ErrorMessage);
        }
    }
}
