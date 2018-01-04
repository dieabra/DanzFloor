using DanzFloor.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.DTO
{
    public class PersonaDTO: EntidadDTO
    {
        public PersonaDTO() { }

        public PersonaDTO(Persona modelo) : base(modelo)
        {
            UsuarioApplicacionId = modelo.UsuarioApplicacion.Id;
            Apellido = modelo.Apellido;
            DNI = modelo.DNI;
            Domicilio = modelo.Domicilio;
            Telefono = modelo.Telefono;
            Celular = modelo.Celular;
            FechaNacimiento = modelo.FechaNacimiento;
            Email = modelo.Email;
            SexoId = modelo.Sexo.Id;
        }

        [Display(Name = "Usuario", Description = "Usuario del Sistema")]
        public string UsuarioApplicacionId { get; set; }

        [Display(Name = "Apellido")]

        public string Apellido { get; set; }

        [Display(Name = "DNI", Description = "Documento Nacional de Identidad.")]
        public int DNI { get; set; }

        [Display(Name = "Domicilio")]
        [MaxLength(100, ErrorMessage = "El Domicilio debe tener como máximo 100 caracteres")]
        public string Domicilio { get; set; }

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
    }
}