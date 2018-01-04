using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Requests
{
    public class requestRecuperarPassword
    {
        public Guid token { get; set; }

        public string password { get; set; }
        public string confirmPassword { get; set; }
    }
}