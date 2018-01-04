using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.Dominio.Usuarios
{
    public class Clubber:Usuario
    {
        public virtual List<UsuarioSocial> CredencialesSociales { get; set; }
    }
}