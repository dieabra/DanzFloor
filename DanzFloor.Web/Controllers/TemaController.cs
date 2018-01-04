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
    public class TemaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Index()
        {
            ViewBag.esCaracteristica = false;
            return View(new Repositorio<Tema>(db).TraerTodos().OrderBy(x => x.Nombre));
        }

        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Create()
        {
            return View(new TemaViewModel(db));
        }
        
        [HttpPost]
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Create(TemaViewModel viewModel)
        {            
            if (ModelState.IsValid)
            {
                var temaCrear = new Tema(viewModel, db);
                new Repositorio<Tema>(db).Crear(temaCrear);
                
                return RedirectToAction("Index");
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

            Tema temaModificar = new Repositorio<Tema>(db).Traer(id.Value);
            
            if (temaModificar == null)
            {
                return HttpNotFound();
            }

            var temaViewModel = new TemaViewModel(temaModificar, db);
            
            return View(temaViewModel);
        }

        [HttpPost]
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Edit(TemaViewModel viewModel)
        {

            if (ModelState.IsValid)
            {

                Tema tema = new Repositorio<Tema>(db).Traer(viewModel.Id);

                tema.Modificar(viewModel, db);
                new Repositorio<Tema>(db).Modificar(tema);

                return RedirectToAction("Index");
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
                var tema = new Repositorio<Tema>(db).Traer(id.Value);
                if (tema == null)
                    return null;
                else
                    new Repositorio<Tema>(db).Eliminar(tema);

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