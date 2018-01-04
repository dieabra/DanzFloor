using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.Autenticacion
{
    public class GoogleResponseValidationToken
    {
        public string azp { get; set; }
        public string aud { get; set; }
        public string sub { get; set; }
        public string email { get; set; }
        public string email_verified { get; set; }
        public string iss { get; set; }
        public string iat { get; set; }
        public string exp { get; set; }
        public string name { get; set; }
        public string picture { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string locale { get; set; }
        public string alg { get; set; }
        public string kid { get; set; }
    }
}