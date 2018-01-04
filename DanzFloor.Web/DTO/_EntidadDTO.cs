using DanzFloor.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.DTO
{
    public class EntidadDTO
    {
        public EntidadDTO() { }

        public EntidadDTO(Entidad modelo)
        {
            Id = modelo.Id;
            Nombre = modelo.Nombre;
            FechaCreacion = modelo.FechaEdicion;
            FechaEdicion = modelo.FechaEdicion;
            Eliminado = modelo.Eliminado;
        }

        public Guid Id { get; set; }

        public string Nombre { get; set; }
        
        public DateTime FechaCreacion { get; set; }

        public DateTime FechaEdicion { get; set; }
        
        public bool Eliminado { get; set; }
    }
}