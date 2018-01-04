using DanzFloor.Web.Datos;
using DanzFloor.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DanzFloor.Web.Models.ViewModels.Frontend
{
    public class RegisterFrontViewModel:FrontendViewModel
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        public RegisterFrontViewModel()
        {
        }

        public RegisterFrontViewModel(Persona model)
        {
            Id = model.Id;
            IdUsuarioApplicacion = model.UsuarioApplicacion.Id;
            Name = model.UsuarioApplicacion.Name;
            Lastname = model.UsuarioApplicacion.Lastname;
            Email = model.UsuarioApplicacion.Email;
        }
        
        [Required(ErrorMessage = "Debe ingresar a que cliente pertenecerá el usuario.")]
        public Guid ClienteId { get; set; }

        public Guid Id { get; set; }

        public string IdUsuarioApplicacion { get; set; }

        [Display(Name = "UserRolesSelected")]
        public IList<string> UserRolesSelected { get; set; }

        [NotMapped]
        public List<SelectListItem> PerfilesUsuarios
        {
            get
            {
                List<SelectListItem> listItems = new List<SelectListItem>();
                var aspNetRoles = ApplicationDbContext.Create().Roles.ToList();

                aspNetRoles.ForEach(x => { listItems.Add(new SelectListItem { Text = x.Name, Value = x.Id }); });
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


        public string IdSocial { get; set; }

        [NotMapped]
        [Display(Name = "ResetPassword")]
        public bool ResetPassword { get; set; }


        [Display(Name = "Email")]
        [Required(ErrorMessage = "Debe ingresar el email.")]
        [EmailAddress(ErrorMessage = "El formato del mail es incorrecto.")]
        public string Email { get; set; }

        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Confirmar contraseña")]
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
}