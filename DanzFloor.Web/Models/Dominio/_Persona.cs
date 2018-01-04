using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DanzFloor.Web.Models
{
    public abstract class Persona : EntidadConArchivo
    {

        [Display(Name = "Usuario", Description = "Usuario del Sistema")]
        public virtual ApplicationUser UsuarioApplicacion { get; set; }

        [Display(Name = "Apellido")]
       
        public string Apellido { get; set; }

        [Display(Name = "DNI", Description = "Documento Nacional de Identidad.")]
        public int DNI { get; set; }

        [Display(Name = "Domicilio")]
        [MaxLength(100, ErrorMessage = "El Domicilio debe tener como máximo 100 caracteres")]
        public string Domicilio { get; set; }
        
        [Display(Name = "Localidad")]
        [MaxLength(100, ErrorMessage = "La localidad debe tener como máximo 100 caracteres")]
        public string Localidad { get; set; }

        [Display(Name = "Teléfono", Description = "Teléfono Fijo")]
        public string Telefono { get; set; }

        [Display(Name = "Teléfono Celular", Description = "Teléfono Celular")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "El teléfono debe tener entre 8 y 20 caracteres.")]
        public string Celular { get; set; }

        [Display(Name = "Fecha de Nacimiento", Description = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [EmailAddress]
        [MaxLength(200, ErrorMessage = "El Email debe tener como máximo 200 caracteres")]
        public string Email { get; set; }


        public Guid SexoId { get; set; }
        public virtual Sexo Sexo { get; set; }

        [NotMapped]
        public string NombreYApellido { get { return Apellido + ", " + Nombre; } }

    }
}