using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.ViewModels
{
    public class RegisterUserFrontendViewModel
    {
        public string Nombre { get; set; }
        public string Mail { get; set; }
        public string IdSocial { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}