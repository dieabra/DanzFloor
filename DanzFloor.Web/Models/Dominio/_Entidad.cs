using DanzFloor.Web.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DanzFloor.Web.Models
{
    public abstract class Entidad : IEntidad
    {

        [ScaffoldColumn(false)]
        private Guid guid { get; set; }
        //private Guid guid = default(Guid);

        [Key]
        public Guid Id
        {
            get
            {
                if (this.guid == default(Guid) || this.guid == null)
                    this.guid = SequentialGuidGenerator.NewSequentialGuid(SequentialGuidType.SequentialAtEnd);
                return guid;
            }

            set { this.guid = value; }
        }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar el Nombre")]
        [MaxLength(100, ErrorMessage = "El nombre debe tener como máximo 100 caracteres")]
        public string Nombre { get; set; }

        //[ScaffoldColumn(false)]
        //[Display(Name = "Autor")]
        //[Required(ErrorMessage = "Debe ingresar el Autor")]
        //public virtual Persona Autor { get; set; }

        [ScaffoldColumn(false)]
        private DateTime fechaCreacion = default(DateTime);

        [ScaffoldColumn(false)]
        [Display(Name = "Fecha de creación")]
        public DateTime FechaCreacion
        {
            get
            {
                return (this.fechaCreacion == default(DateTime))
                   ? DateTime.Now
                   : this.fechaCreacion;
            }

            set { this.fechaCreacion = value; }
        }

        [ScaffoldColumn(false)]
        private DateTime fechaEdicion = default(DateTime);

        [ScaffoldColumn(false)]
        [Display(Name = "Fecha de última edición")]
        public DateTime FechaEdicion
        {
            get
            {
                return (this.fechaEdicion == default(DateTime))
                   ? DateTime.Now
                   : this.fechaEdicion;
            }

            set { this.fechaEdicion = value; }
        }

        [Display(Name = "Eliminado")]
        [Required]
        public bool Eliminado { get; set; }
    }


    public class EntidadComparer : IEqualityComparer<Entidad>
    {
        public bool Equals(Entidad x, Entidad y)
        {
            return x.Id.Equals(y.Id);
        }

        public int GetHashCode(Entidad obj)
        {
            return obj.GetHashCode();
        }
    }
}