using DanzFloor.Web.Models;
using System;
using System.Web.Mvc;

namespace DanzFloor.Web.Controllers
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }

}