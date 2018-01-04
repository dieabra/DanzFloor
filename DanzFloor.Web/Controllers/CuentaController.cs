using DanzFloor.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DanzFloor.Web.Datos;
using Newtonsoft.Json;
using DanzFloor.Web.Models.ViewModels;
using DanzFloor.Web.DTO;
using DanzFloor.Web.Models.Enum;
using DanzFloor.Web.Models.Constantes;
using System.Net.Http;
using DanzFloor.Web.Models.Dominio;
using DanzFloor.Web.Models.Dominio.Usuarios;

namespace DanzFloor.Web.Controllers
{
    public class CuentaController : BaseCuentaController
    {
        public CuentaController() : base()
        {
        }

        public CuentaController(ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(userManager, signInManager)
        {
        }

        [AllowCrossSiteJson]
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult PromotorIndex()
        {
            var db = new ApplicationDbContext();

            var resultado = new Repositorio<Promotor>(db).TraerTodos().ToList();

            return View(resultado);
        }

        //
        // GET: /Account/Register
        [AllowCrossSiteJson]
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Register()
        {
            ViewBag.Name = new SelectList(ApplicationDbContext.Create().Roles.ToList(), "Name", "Name");

            RegisterViewModel registerViewModel = new RegisterViewModel() { UserRolesSelected = new List<string>(), ResetPassword = true };
            ViewBag.Title = "Registrar Nuevo Usuario";

            return View(registerViewModel);
        }

        [AllowCrossSiteJson]
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult RegisterClubber()
        {
            ViewBag.Name = new SelectList(ApplicationDbContext.Create().Roles.ToList(), "Name", "Name");

            RegisterViewModel registerViewModel = new RegisterViewModel() { UserRolesSelected = new List<string>(), ResetPassword = true };
            ViewBag.Title = "Registrar Nuevo Usuario";

            return View(registerViewModel);
        }

        //
        // POST: /Account/Register
        [AllowCrossSiteJson]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleConst.Administrador)]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (this.RegisterUser(model))
                    return RedirectToAction("ManageUsers", "Cuenta", new { result = "OK" });

                //AddErrors(result);
            }
            // If we got this far, something failed, redisplay form
            model.ResetPassword = true;
            return View(model);
        }
        
        // GET: /Manage/Edit
        [AllowCrossSiteJson]
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Edit(Guid id)
        {
            var usuario = new Repositorio<Persona>(db).Traer(id);
            var registerViewModel = new RegisterViewModel(usuario);

            List<string> seleccionados = UserManager.GetRoles(usuario.UsuarioApplicacion.Id).ToList();
            
            ViewBag.Title = "Editar Usuario";
            return View("../Cuenta/Register", registerViewModel);
        }

        [AllowCrossSiteJson]
        [HttpPost]
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Edit(RegisterViewModel usuarioModificado)
        {
            var appUser = UserManager.FindById(usuarioModificado.IdUsuarioApplicacion);

            if (ModelState.ContainsKey("Email") && !ModelState["Email"].Errors.Any() && 
                ModelState.ContainsKey("ClienteId") && !ModelState["ClienteId"].Errors.Any())
            {

                if (ModificarApplicationUser(usuarioModificado))
                {
                    var usuario = new Repositorio<Usuario>(db).Traer(usuarioModificado.Id);
                    
                    new Repositorio<Persona>(db).Modificar(usuario);

                    return RedirectToAction("ManageUsers", "Cuenta", new { Message = "UpdateUserSuccess" });
                }
                
            }
            return RedirectToRoute("Default", new { Id = usuarioModificado.Id, Message = "UpdateUserError" });

        }

        public JsonResult RestaurarContrasena(Guid id)
        {
            this.RestaurarPassword(id);

            return Json(true,JsonRequestBehavior.AllowGet);
        }
        
        // GET: /Manage/Edit
        [AllowCrossSiteJson]
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Delete(Guid id)
        {
            try
            {
                var usuario = new Repositorio<Persona>(db).Traer(id);
                //usuario.Cliente = usuario.Cliente;

                usuario.UsuarioApplicacion.UserName = usuario.UsuarioApplicacion.UserName + " | ELIMINADO " + GenerarRandomPass();
                usuario.UsuarioApplicacion.Email = usuario.UsuarioApplicacion.UserName;
                new Repositorio<Persona>(db).Modificar(usuario);

                new Repositorio<Persona>(db).Eliminar(usuario);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return RedirectToRoute("Default", new { Id = id, Message = "UpdateUserError" });
            }
            
            return RedirectToAction("ManageUsers", "Cuenta", new { Message = "DeleteUserSuccess" });
        }
        
        #region API
        
        [Authorize]
        public async Task<dynamic> Proxy(string url)
        {
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(url))
            using (HttpContent content = response.Content)
            {
                // ... Read the string.
                return await content.ReadAsStringAsync();
            }
        }

        #endregion
    }

}