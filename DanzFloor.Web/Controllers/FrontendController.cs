using System.Web.Mvc;
using DanzFloor.Web.Models;
using DanzFloor.Web.Models.ViewModels;
using System;
using DanzFloor.Web.Datos;
using DanzFloor.Web.Models.Dominio;
using System.Linq;
using Microsoft.AspNet.Identity;
using DanzFloor.Web.Models.Enum;
using DanzFloor.Web.Helpers;
using DanzFloor.Web.Models.ViewModels.Frontend;
using System.Threading.Tasks;
using System.Collections.Generic;
using Postal;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Configuration;
using DanzFloor.Web.Models.Dominio.Usuarios;
using Microsoft.AspNet.Identity.Owin;
using System.Net;
using System.Collections.Specialized;
using DanzFloor.Web.Models.Autenticacion;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DanzFloor.Web.Controllers
{
    [Authorize]
    public class FrontendController : BaseCuentaController
    {
        public ActionResult Index(Guid? destacadoId)
        {
            IndexViewModel viewModel;

            if (destacadoId == null)
                viewModel = new IndexViewModel(db);
            else
                viewModel = new IndexViewModel(destacadoId.Value, db);


            return View(viewModel);
        }

        public ActionResult IndexMobile()
        {
            var viewModel = new IndexViewModel(db);
            return View(viewModel);
        }


        public ActionResult MiPerfil()
        {
            var viewModel = new FrontendViewModel(db);

            return View(viewModel);
        }


        public ActionResult DetalleUsuario(Guid id)
        {
            var usuario = new Repositorio<Usuario>(db).Traer(id);
            var viewModel = new RegisterFrontViewModel();

            List<string> seleccionados = UserManager.GetRoles(usuario.UsuarioApplicacion.Id).ToList();

            viewModel.UserRolesSelected = viewModel.Perfiles
                .Where(x => seleccionados.Contains(x.Text))
                .Select(x => x.Value).ToList();

            viewModel.RegenerarFrontend(db);
            ViewBag.Title = "Detalle Usuario";
            return View(viewModel);
        }
        


        

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public new async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            ViewBag.urlFacebookRedirect = "https://www.facebook.com/v2.9/dialog/oauth?%20client_id=512859328903997%20&redirect_uri=" + WebConfigurationManager.AppSettings["Core"] + WebConfigurationManager.AppSettings["urlFacebookRedirect"] + "&scope=email";
            ViewBag.urlInstagramRedirect = "https://www.instagram.com/oauth/authorize/?client_id=" + SocialIds.InstagramClientId + "&redirect_uri=" + WebConfigurationManager.AppSettings["Core"] + WebConfigurationManager.AppSettings["urlInstagramRedirect"] + "&response_type=code";

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        public ActionResult EliminarUsuario(Guid id)
        {
            var usuarioActual = UsuarioClienteHelper.Actual(db);

            //if (!usuarioActual.EsAdministrador)
            //{
            //    throw new Exception("SEGURIDAD: El usuario con id: " + usuarioActual.Id + " intentó entrar a la sección de creación de usuarios.");
            //}

            try
            {
                var usuario = new Repositorio<Usuario>(db).Traer(id);
                new Repositorio<Usuario>(db).Eliminar(usuario);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return RedirectToRoute("Default", new { Id = id, Message = "UpdateUserError" });
            }

            return Json(new
            {
                Resultado = Resultado.OK,
                Mensaje = "Se ha realizado el proceso correctamente."
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AltaUsuario()
        {
            var usuarioActual = UsuarioClienteHelper.Actual(db);

            //if (!usuarioActual.EsAdministrador)
            //{
            //    throw new Exception("SEGURIDAD: El usuario con id: " + usuarioActual.Id + " intentó entrar a la sección de creación de usuarios.");
            //}

            ViewBag.Name = new SelectList(ApplicationDbContext.Create().Roles.ToList(), "Name", "Name");

            RegisterFrontViewModel viewModel = new RegisterFrontViewModel() { UserRolesSelected = new List<string>(), ResetPassword = true };

            return View("Usuario", viewModel);
        }


        [HttpGet]
        public ActionResult EditarUsuario(Guid id)
        {
            var usuarioActual = UsuarioClienteHelper.Actual(db);

            //if (!usuarioActual.EsAdministrador && !usuarioActual.EsGerenteSucursal)
            //{
            //    throw new Exception("SEGURIDAD: El usuario con id: " + usuarioActual.Id + " intentó entrar a la sección de creación de usuarios.");
            //}

            var usuario = new Repositorio<Usuario>(db).Traer(id);
            var viewModel = new RegisterFrontViewModel(usuario);

            List<string> seleccionados = UserManager.GetRoles(usuario.UsuarioApplicacion.Id).ToList();

            viewModel.UserRolesSelected = viewModel.Perfiles
                .Where(x => seleccionados.Contains(x.Text))
                .Select(x => x.Value).ToList();

            viewModel.RegenerarFrontend(db);
            ViewBag.Title = "Editar Usuario";

            return View("Usuario", viewModel);
        }

        [HttpPost]
        public ActionResult EditarUsuario(RegisterFrontViewModel viewModel)
        {

            ModelState["Password"].Errors.Clear();

            var usuarioActual = UsuarioClienteHelper.Actual(db);

            //if (!usuarioActual.EsAdministrador && !usuarioActual.EsGerenteSucursal)
            //{
            //    throw new Exception("SEGURIDAD: El usuario con id: " + usuarioActual.Id + " intentó entrar a la sección de creación de usuarios.");
            //}

            var appUser = UserManager.FindById(viewModel.IdUsuarioApplicacion);

            if (ModelState.IsValid)
            {

                if (ModificarApplicationUser(viewModel))
                {
                    var usuario = new Repositorio<Usuario>(db).Traer(viewModel.Id);

                    new Repositorio<Usuario>(db).Modificar(usuario);

                    return RedirectToAction("MiPerfil", "Frontend", new { Message = "UpdateUserSuccess" });
                }

            }

            return View("Usuario", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AltaUsuario(RegisterFrontViewModel viewModel)
        {
            var usuarioActual = UsuarioClienteHelper.Actual(db);

            //if (!usuarioActual.EsAdministrador)
            //{
            //    throw new Exception("SEGURIDAD: El usuario con id: " + usuarioActual.Id + " intentó entrar a la sección de creación de usuarios.");
            //}

            if (ModelState.IsValid)
            {
                if (this.RegisterUser(viewModel))
                    return RedirectToAction("MiPerfil", "Frontend");
            }
            viewModel.ResetPassword = true;
            return View("Usuario", viewModel);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
