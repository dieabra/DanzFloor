using System.ComponentModel.DataAnnotations;

namespace DanzFloor.Web.ViewModels
{
    public class LoginMobileRequestVM
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo clave es Requerido")]
        [StringLength(100, ErrorMessage = "La {0} debe tener un mínimo de {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Clave")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Versión de Applicación")]
        public string Version { get; set; }
    }
}