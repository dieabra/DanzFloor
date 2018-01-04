using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.ViewModels
{
    public class RecuperarPasswordViewModel
    {
        public Guid Token { get; set; }
        public string UsuarioId { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string Error { get; set; }
    }
}