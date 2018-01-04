using DanzFloor.Web.Datos;
using DanzFloor.Web.Models;
using DanzFloor.Web.Models.Dominio;
using DanzFloor.Web.Models.Enum;
using DanzFloor.Web.Models.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DanzFloor.Web.Controllers
{
    public class GrupoTagController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Index()
        {
            ViewBag.esCaracteristica = false;
            return View(new Repositorio<GrupoTag>(db).TraerTodos().OrderBy(x => x.Nombre));
        }

        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Create(bool caracteristica)
        {
            return View(new GrupoTagViewModel(db, caracteristica));
        }
        
        [HttpPost]
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Create(GrupoTagViewModel viewModel)
        {            
            if (ModelState.IsValid)
            {
                var grupoTagCrear = new GrupoTag(viewModel, db);
                new Repositorio<GrupoTag>(db).Crear(grupoTagCrear);
                
                return RedirectToAction("Index", new { caracteristica = viewModel.EsCaracteristica.ToString() });
            }

            viewModel.RegenerarVista(db);

            return View(viewModel);
        }

        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GrupoTag grupoTagModificar = new Repositorio<GrupoTag>(db).Traer(id.Value);
            
            if (grupoTagModificar == null)
            {
                return HttpNotFound();
            }

            var grupoTagViewModel = new GrupoTagViewModel(grupoTagModificar, db);
            
            return View(grupoTagViewModel);
        }

        [HttpPost]
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Edit(GrupoTagViewModel viewModel)
        {

            if (ModelState.IsValid)
            {

                GrupoTag grupoTag = new Repositorio<GrupoTag>(db).Traer(viewModel.Id);

                grupoTag.Modificar(viewModel, db);
                new Repositorio<GrupoTag>(db).Modificar(grupoTag);

                return RedirectToAction("Index", new { caracteristica = viewModel.EsCaracteristica.ToString() });
            }

            viewModel.RegenerarVista(db);

            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Delete(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    return null;// new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var grupoTag = new Repositorio<GrupoTag>(db).Traer(id.Value);
                if (grupoTag == null)
                    return null;
                else
                    new Repositorio<GrupoTag>(db).Eliminar(grupoTag);

                return Json(new
                {
                    Resultado = Resultado.OK,
                    Mensaje = ""
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Resultado = Resultado.Error,
                    Mensaje = ""
                }, JsonRequestBehavior.AllowGet);
            }
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