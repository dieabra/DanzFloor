using DanzFloor.Web.Datos;
using DanzFloor.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DanzFloor.Web.Controllers
{
    public class SexoController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sexo
        [AllowCrossSiteJson]
        public ActionResult Index()
        {
            //var sexo = new Sexo { Nombre = "Prueba" };
            //new Repositorio<Sexo>(db).Crear(sexo);
            var sexos = new Repositorio<Sexo>(db).TraerTodos();

            return Json(sexos, JsonRequestBehavior.AllowGet);
        }


        // GET: Sexo/Details/5
        [AllowCrossSiteJson]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Sexo/Create
        [AllowCrossSiteJson]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sexo/Create
        [AllowCrossSiteJson]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sexo/Edit/5
        [AllowCrossSiteJson]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Sexo/Edit/5
        [AllowCrossSiteJson]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sexo/Delete/5
        [AllowCrossSiteJson]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Sexo/Delete/5
        [AllowCrossSiteJson]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
