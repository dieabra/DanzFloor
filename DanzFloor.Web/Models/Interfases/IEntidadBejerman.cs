using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.Dominio
{
    public interface IEntidadBejerman
    {
        bool Habilitado { get; set; }

        bool Completo { get; set; }

        string BejermanId { get; set; }

        string Estado { get; }
    }
}