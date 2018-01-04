using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using DanzFloor.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DanzFloor.Web.Datos;
using DanzFloor.Web.Models.Dominio;
using DanzFloor.Web.Models.Dominio.Usuarios;

namespace DanzFloor.Web.Helpers
{
    public class UsuarioClienteHelper
    {
        public static Persona Actual(ApplicationDbContext db)
        {
            var usuarioAplicacionId = HttpContext.Current.User.Identity.GetUserId();
            return new Repositorio<Persona>(db).TraerTodos()
                .Single(x => x.UsuarioApplicacion.Id == usuarioAplicacionId);
        }

        public static string[] GetRoles(ApplicationUser user)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            return userManager.GetRoles(user.Id).ToArray();
        }
    }
}