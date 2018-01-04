using DanzFloor.Web.Helpers;
using DanzFloor.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Web.Configuration;
using System.Web.Mvc;
using Postal;

namespace DanzFloor.Web.Controllers
{
    public class MailController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        [HttpPost]
        [Authorize]
        public JsonResult EnviarMailRecuperarPassWord()
        {
            string mail = System.Web.HttpContext.Current.Request.Form["mail"];
           // string mail = Request.QueryString["mail"].ToString();
            UserManager<ApplicationUser> um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = um.FindByEmail(mail);
            //var repositorio = new Repositorio<Supervisor>(db);
            //var user = repositorio.TraerTodos().SingleOrDefault(a => a.Usuario.UserName == mail);
            if (user != null)
            {
                ViewBag.To = mail;
                ViewBag.Subject = "Recuperar Usuario";

                string link = WebConfigurationManager.AppSettings["Core"] + WebConfigurationManager.AppSettings["RecuperarPasswordLink"];

                var newToken = SequentialGuidGenerator.NewSequentialGuid(SequentialGuidType.SequentialAtEnd);
                user.TokenFechaVencimiento = DateTime.Today.AddDays(7);
                user.Token = newToken;
                um.Update(user);
                try
                {
                    dynamic email = new Email("EnviarMailRecuperarPassWord");
                    email.Link = link.Replace("[[token]]", newToken.ToString().ToUpper()).Replace("[[usuarioId]]", user.Id);
                    email.Subject = "Recuperar Usuario";
                    email.To = mail;
                    email.Send();
                }
                catch (Exception ex)
                {
                    return Json("Error: no se ha podido enviar el mail con los pasos a seguir. Intente más tarde.", JsonRequestBehavior.AllowGet);
                }
                return Json("Se ha enviado un email a su casilla de email con los pasos a seguir", JsonRequestBehavior.AllowGet);

            }

            return null;
        }
    }
}