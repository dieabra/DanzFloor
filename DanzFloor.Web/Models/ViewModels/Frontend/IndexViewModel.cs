using DanzFloor.Web.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanzFloor.Web.Models.ViewModels.Frontend
{
    public class IndexViewModel : FrontendViewModel
    {
        public IndexViewModel()
        {
        }

        public IndexViewModel(Guid destacadoId, ApplicationDbContext db)
        {
            Destacado = new Repositorio<Destacado>(db).Traer(destacadoId);
        }

        public IndexViewModel(ApplicationDbContext db)
            : base(db)
        {
            
            Destacado = Destacado.ObtenerDestacado(new List<Guid>(), db);

        }

        public Destacado Destacado { get; set; }
    }
}