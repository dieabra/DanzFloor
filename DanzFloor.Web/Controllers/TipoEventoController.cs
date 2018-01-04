using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DanzFloor.Web.Models;
using DanzFloor.Web.Models.Dominio;
using DanzFloor.Web.Datos;
using DanzFloor.Web.Models.Enum;

namespace DanzFloor.Web.Controllers
{
    public class TipoEventoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Index()
        {
            return View(new Repositorio<TipoEvento>(db).TraerTodos().ToList());
        }

        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoEvento tipoEvento = new Repositorio<TipoEvento>(db).Traer(id.Value);
            if (tipoEvento == null)
            {
                return HttpNotFound();
            }
            return View(tipoEvento);
        }

        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Create()
        {
            return View(new TipoEvento());
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Create([Bind(Include = "Id,Nombre,FechaCreacion,FechaEdicion,Eliminado")] TipoEvento tipoEvento)
        {
            if (ModelState.IsValid)
            {
                new Repositorio<TipoEvento>(db).Crear(tipoEvento);
                return RedirectToAction("Index");
            }

            return View(tipoEvento);
        }

        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoEvento tipoEvento = new Repositorio<TipoEvento>(db).Traer(id.Value);
            if (tipoEvento == null)
            {
                return HttpNotFound();
            }
            return View(tipoEvento);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Edit([Bind(Include = "Id,Nombre,FechaCreacion,FechaEdicion,Eliminado")] TipoEvento tipoEvento)
        {
            if (ModelState.IsValid)
            {
                new Repositorio<TipoEvento>(db).Modificar(tipoEvento);
                return RedirectToAction("Index");
            }
            return View(tipoEvento);
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
                TipoEvento tipoEvento = new Repositorio<TipoEvento>(db).Traer(id.Value);
                if (tipoEvento == null)
                    return null;
                else
                    new Repositorio<TipoEvento>(db).Eliminar(tipoEvento);

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
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TipoEvento tipoEvento = new Repositorio<TipoEvento>(db).Traer(id);
            new Repositorio<TipoEvento>(db).Eliminar(tipoEvento);

            return RedirectToAction("Index");
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
