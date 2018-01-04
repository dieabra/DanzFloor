using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.Autenticacion
{
    public class InstagramResponseValidationCode
    {
        public string access_token { get; set; }
        public InstagramUser user { get; set; }
    }
}