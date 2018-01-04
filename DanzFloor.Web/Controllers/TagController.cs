﻿using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DanzFloor.Web.Models;
using DanzFloor.Web.Models.Dominio;
using DanzFloor.Web.Datos;
using DanzFloor.Web.Models.Enum;

namespace DanzFloor.Web.Controllers
{
    public class TagController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Index()
        {
            return View(new Repositorio<Tag>(db).TraerTodos().ToList());
        }

        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = new Repositorio<Tag>(db).Traer(id.Value);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Create()
        {
            return View(new Tag());
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Create([Bind(Include = "Id,Nombre,FechaCreacion,FechaEdicion,Eliminado,VisibleFront")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                new Repositorio<Tag>(db).Crear(tag);
                return RedirectToAction("Index");
            }

            return View(tag);
        }

        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = new Repositorio<Tag>(db).Traer(id.Value);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Edit([Bind(Include = "Id,Nombre,FechaCreacion,FechaEdicion,Eliminado,VisibleFront")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                new Repositorio<Tag>(db).Modificar(tag);
                return RedirectToAction("Index");
            }
            return View(tag);
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
                Tag tag = new Repositorio<Tag>(db).Traer(id.Value);
                if (tag == null)
                    return null;
                else
                    new Repositorio<Tag>(db).Eliminar(tag);

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
            Tag tag = new Repositorio<Tag>(db).Traer(id);
            new Repositorio<Tag>(db).Eliminar(tag);

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
