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
    public class VenueController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Index()
        {
            return View(new Repositorio<Venue>(db).TraerTodos().OrderBy(x => x.Nombre));
        }

        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Create()
        {
            return View(new VenueViewModel(db));
        }
        
        [HttpPost]
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Create(VenueViewModel viewModel)
        {            
            if (ModelState.IsValid)
            {
                var venueCrear = new Venue(viewModel, db);
                new Repositorio<Venue>(db).Crear(venueCrear);
                
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

            Venue venueModificar = new Repositorio<Venue>(db).Traer(id.Value);
            
            if (venueModificar == null)
            {
                return HttpNotFound();
            }

            var venueViewModel = new VenueViewModel(venueModificar, db);

            venueViewModel.RegenerarVista(db);

            return View(venueViewModel);
        }

        [HttpPost]
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Edit(VenueViewModel viewModel)
        {

            if (ModelState.IsValid)
            {

                Venue venue = new Repositorio<Venue>(db).Traer(viewModel.Id);

                venue.Modificar(viewModel, db);
                new Repositorio<Venue>(db).Modificar(venue);

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
                var venue = new Repositorio<Venue>(db).Traer(id.Value);
                if (venue == null)
                    return null;
                else
                    new Repositorio<Venue>(db).Eliminar(venue);

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