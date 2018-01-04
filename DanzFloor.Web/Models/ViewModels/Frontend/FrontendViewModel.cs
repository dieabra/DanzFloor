using DanzFloor.Web.Datos;
using DanzFloor.Web.Models;
using DanzFloor.Web.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.ViewModels.Frontend
{
    public class FrontendViewModel : PaginadoViewModel
    {
        public FrontendViewModel()
        {
            InicializarEntidad();
        }

        public FrontendViewModel(ApplicationDbContext db)
        {
            RegenerarFrontend(db);
        }

        public void RegenerarFrontend(ApplicationDbContext db)
        {
            InicializarEntidad();
            var usuarioAplicacionId = HttpContext.Current.User.Identity.GetUserId();
            GruposTag = new Repositorio<GrupoTag>(db).TraerTodos().Where(x => x.VisibleFront && x.EsCaracteristica).ToList();
            
        }

        public void InicializarEntidad()
        {
            GruposTag = new List<GrupoTag>();
        }
        
        
        public List<GrupoTag> GruposTag { get; set; }
    }
}