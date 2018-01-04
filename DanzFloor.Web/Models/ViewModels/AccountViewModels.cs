using System.Collections.Generic;


using System.Web.Mvc;

using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DanzFloor.Web.Models.Dominio;
using System;
using DanzFloor.Web.Datos;

namespace DanzFloor.Web.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Debe ingresar el email.")]
        [EmailAddress(ErrorMessage = "El formato del mail es incorrecto.")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Recordar el navegador?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Debe ingresar el email.")]
        [EmailAddress(ErrorMessage = "El formato del mail es incorrecto.")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Debe ingresar el mail.")]
        public string Email { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Debe ingresar la contraseña.")]
        [DataType(DataType.Password, ErrorMessage = "No es correcta la confirmación del mail.")]
        public string Password { get; set; }

        [Display(Name = "Recordar")]
        public bool RememberMe { get; set; }

        public bool MostrarPopUpLoginFront { get; set; }
    }

    public class RegisterViewModel 
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        
        public RegisterViewModel()
        {
            //Clientes = new Repositorio<Cliente>(db).TraerTodos().Where(x => x.Completo);
        }

        public RegisterViewModel(Persona model)
        {
            //Clientes = new Repositorio<Cliente>(db).TraerTodos().Where(x => x.Completo);

            Id = model.Id;
            IdUsuarioApplicacion = model.UsuarioApplicacion.Id;
            Name = model.UsuarioApplicacion.Name;
            Lastname = model.UsuarioApplicacion.Lastname;
            Email = model.UsuarioApplicacion.Email;
            //ClienteId = model.Cliente.Id;
        }

        //public IEnumerable<Cliente> Clientes { get; set; }

        [Required(ErrorMessage = "Debe ingresar a que cliente pertenecerá el usuario.")]
        public Guid ClienteId { get; set; }

        public Guid Id { get; set; }

        public string IdUsuarioApplicacion { get; set; }

        [Display(Name = "UserRolesSelected")]
        public List<string> UserRolesSelected { get; set; }

        [NotMapped]
        public List<SelectListItem> PerfilesUsuarios
        {
            get
            {
                List<SelectListItem> listItems = new List<SelectListItem>();                
                var aspNetRoles = ApplicationDbContext.Create().Roles.ToList();

                aspNetRoles.ForEach(x => { listItems.Add(new SelectListItem {Text=x.Name,Value=x.Id}); });             
                return listItems;
            }
        }

        public IEnumerable<DanzFloor.Web.Models.ViewModels.UserRole> Perfiles
        {
            get
            {                               
                return PerfilesUsuarios.ToList().Select(x => new DanzFloor.Web.Models.ViewModels.UserRole() { Text = x.Text, Value = x.Value, Selected = x.Selected }).ToList();                
            }
        }

        [NotMapped]
        [Display(Name = "ResetPassword")]
        public bool ResetPassword { get; set; }
      

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Debe ingresar el email.")]
        [EmailAddress(ErrorMessage = "El formato del mail es incorrecto.")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Debe ingresar la contraseña.")]
        //[StringLength(100, ErrorMessage = "El {0} debe tener al menos {2} caracteres de longitud.", MinimumLength = 6)]
        //[DataType(DataType.Password, ErrorMessage = "El formato de la contraseña es incorrecto.")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Confirmar contraseña")]
        //[DataType(DataType.Password, ErrorMessage = "El formato de la contraseña es incorrecto.")]
        //[System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "El password y su confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar el Nombre.")]
        [MaxLength(50, ErrorMessage = "El Nombre debe tener como máximo 50 caracteres")]
        public string Name { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "Debe ingresar el Apellido.")]
        [MaxLength(50, ErrorMessage = "El Apellido debe tener como máximo 50 caracteres")]
        public string Lastname { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Debe ingresar el email.")]
        [EmailAddress(ErrorMessage = "El formato del mail es incorrecto.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe ingresar la contraseña.")]
        [StringLength(100, ErrorMessage = "El {0} debe tener al menos {2} caracteres de longitud.", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "El formato de la contraseña es incorrecto.")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password, ErrorMessage = "El formato de la contraseña es incorrecto.")]
        [Display(Name = "Confirmar contraseña")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "El password y su confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Debe ingresar el email.")]
        [EmailAddress(ErrorMessage = "El formato del mail es incorrecto.")]
        public string Email { get; set; }
    }






}
