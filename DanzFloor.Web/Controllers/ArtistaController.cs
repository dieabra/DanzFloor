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
    public class ArtistaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Index()
        {
            return View(new Repositorio<Artista>(db).TraerTodos().OrderBy(x => x.Nombre));
        }

        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Create()
        {
            return View(new ArtistaViewModel(db));
        }
        
        [HttpPost]
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Create(ArtistaViewModel viewModel)
        {            
            if (ModelState.IsValid)
            {
                var artistaCrear = new Artista(viewModel, db);
                new Repositorio<Artista>(db).Crear(artistaCrear);
                
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

            Artista artistaModificar = new Repositorio<Artista>(db).Traer(id.Value);
            
            if (artistaModificar == null)
            {
                return HttpNotFound();
            }

            var artistaViewModel = new ArtistaViewModel(artistaModificar, db);

            artistaViewModel.RegenerarVista(db);

            return View(artistaViewModel);
        }

        [HttpPost]
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Edit(ArtistaViewModel viewModel)
        {

            if (ModelState.IsValid)
            {

                Artista artista = new Repositorio<Artista>(db).Traer(viewModel.Id);

                artista.Modificar(viewModel, db);
                new Repositorio<Artista>(db).Modificar(artista);

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
                var artista = new Repositorio<Artista>(db).Traer(id.Value);
                if (artista == null)
                    return null;
                else
                    new Repositorio<Artista>(db).Eliminar(artista);

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