using DanzFloor.Web.Models;
using System;

namespace DanzFloor.Web.Models
{
    public interface IEntidad
    {
        Guid Id { get; set; }
        string Nombre { get; set; }
        DateTime FechaCreacion { get; set; }
        DateTime FechaEdicion { get; set; }
        bool Eliminado { get; set; }

    }
}