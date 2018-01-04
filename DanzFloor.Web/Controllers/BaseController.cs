using DanzFloor.Web.Datos;
using DanzFloor.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq;
using System.Threading;
using System.Net;

namespace DanzFloor.Web.Controllers
{

    public class BaseController : Controller
    {
        public string Contenido { get; set; }

        public BaseController()
        {
            string usuarioId = System.Web.HttpContext.Current.Request.Form["usuarioId"];
            string tokenId = System.Web.HttpContext.Current.Request.Form["tokenId"];
            string version = System.Web.HttpContext.Current.Request.Form["version"];

            Contenido = System.Web.HttpContext.Current.Request.Form["contenido"];

            var modelo = new ApplicationDbContext();
            VersionMobile VersionMobile = new Repositorio<VersionMobile>(modelo).TraerTodos().OrderByDescending(v => v.FechaEdicion).FirstOrDefault();

            if (!string.IsNullOrEmpty(usuarioId) && !string.IsNullOrEmpty(tokenId))
            {
                UserManager<ApplicationUser> um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var user = um.FindById(usuarioId);
                if (user != null)
                {
                    if (VersionMobile.VersionValida(version))
                    {
                        if (user.Token.ToString().Equals(tokenId) && (DateTime.Now <= user.TokenFechaVencimiento))
                        {
                            string[] roles = { "Suscriptor" };

                            FormsAuthentication.SetAuthCookie(user.UserName, true);
                            HttpCookie decryptedCookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

                            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(decryptedCookie.Value);

                            var identity = new GenericIdentity(ticket.Name);
                            var principal = new GenericPrincipal(identity, roles);

                            System.Web.HttpContext.Current.User = principal;
                            Thread.CurrentPrincipal = System.Web.HttpContext.Current.User;
                        }
                        else
                        {
                            System.Web.HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.Unauthorized; //Token invalido
                        }
                    }
                    else
                    {
                        System.Web.HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.NotAcceptable; //Version invalida
                    }
                }
                else
                {
                    System.Web.HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.Forbidden; //Usuario invalido
                }
            }

        }
    }
}