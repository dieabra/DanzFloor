using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace DanzFloor.Web.Models
{
    //public class IndexViewModel
    //{
    //    public bool HasPassword { get; set; }
    //    public IList<UserLoginInfo> Logins { get; set; }
    //    public string PhoneNumber { get; set; }
    //    public bool TwoFactor { get; set; }
    //    public bool BrowserRemembered { get; set; }
    //}

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "El {0} debe tener al menos {2} caracteres de longitud.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva Congtraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme la nueva contraseña")]
        [Compare("NewPassword", ErrorMessage = "El password y su confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Debe ingresar la contraseña.")]
        [StringLength(100, ErrorMessage = "El {0} debe tener al menos {2} caracteres de longitud.", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "El formato de la contraseña es incorrecto.")]
        [Display(Name = "Contraseña actual")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Debe ingresar la contraseña nueva.")]
        [StringLength(100, ErrorMessage = "El {0} debe tener al menos {2} caracteres de longitud.", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "El formato de la contraseña es incorrecto.")]
        [Display(Name = "Nueva Contraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password, ErrorMessage = "El formato de la contraseña es incorrecto.")]
        [Display(Name = "Confirme la nueva contraseña")]
        [Compare("NewPassword", ErrorMessage = "El password y su confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Número de Teléfono")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Número de Teléfono")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}