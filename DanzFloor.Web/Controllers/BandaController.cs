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
    public class BandaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Index()
        {
            return View(new Repositorio<Banda>(db).TraerTodos().OrderBy(x => x.Nombre));
        }

        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Create()
        {
            return View(new BandaViewModel(db));
        }
        
        [HttpPost]
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Create(BandaViewModel viewModel)
        {            
            if (ModelState.IsValid)
            {
                var bandaCrear = new Banda(viewModel, db);
                new Repositorio<Banda>(db).Crear(bandaCrear);
                
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

            Banda bandaModificar = new Repositorio<Banda>(db).Traer(id.Value);
            
            if (bandaModificar == null)
            {
                return HttpNotFound();
            }

            var bandaViewModel = new BandaViewModel(bandaModificar, db);

            bandaViewModel.RegenerarVista(db);

            return View(bandaViewModel);
        }

        [HttpPost]
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Edit(BandaViewModel viewModel)
        {

            if (ModelState.IsValid)
            {

                Banda banda = new Repositorio<Banda>(db).Traer(viewModel.Id);

                banda.Modificar(viewModel, db);
                new Repositorio<Banda>(db).Modificar(banda);

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
                var banda = new Repositorio<Banda>(db).Traer(id.Value);
                if (banda == null)
                    return null;
                else
                    new Repositorio<Banda>(db).Eliminar(banda);

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