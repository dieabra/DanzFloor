using System.Web.Mvc;
using DanzFloor.Web.Models;

namespace DanzFloor.Web.Controllers
{
    public class InicioController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
            return RedirectToAction("LogOff", "Cuenta");
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
