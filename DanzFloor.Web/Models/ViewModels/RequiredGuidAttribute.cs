using System;
using System.ComponentModel.DataAnnotations;

namespace DanzFloor.Web.Models.ViewModels
{
    internal class RequiredGuidAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((Guid)value == Guid.Empty)
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }
    }
}