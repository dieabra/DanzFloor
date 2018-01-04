using DanzFloor.Web.Datos;
using DanzFloor.Web.DTO;
using DanzFloor.Web.Helpers;
using DanzFloor.Web.Models;
using DanzFloor.Web.Models.Dominio;
using DanzFloor.Web.Models.ViewModels.Backend;
using DanzFloor.Web.Models.ViewModels.Backend.Home;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using DanzFloor.Web.Models.ViewModels;
using DanzFloor.Web.Models.Enum;
using DanzFloor.Web.Models.Dominio.Usuarios;

namespace DanzFloor.Web.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        
        [AllowCrossSiteJson]
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Cuenta");
            //else if (!User.IsInRole(RoleConst.Colaborador))
            //{
            //    var usuarioActual = UsuarioClienteHelper.Actual(db);

            //    //if(usuarioActual.Cliente.Habilitado)
            //        return RedirectToAction("Index", "Frontend");
            //    //else
            //    //    return RedirectToAction("Logoff", "Cuenta");
            //}
            //var userID = User.Identity.GetUserId();
            //var viewModel = new HomeDashboardViewModel() {
            //    UsuarioActual = new Repositorio<Colaborador>(db).TraerTodos().FirstOrDefault(x => x.UsuarioApplicacion.Id == userID)
            //};
            var viewModel = new HomeDashboardViewModel();
            return View(viewModel);
        }

     

        [AllowCrossSiteJson]

        public ActionResult CSV()
        {
            return (View());
        }

        //[HttpGet]
        //public PartialViewResult _GraficoEvolutivoTicket()
        //{

        //    var viewModel = new GraficoBarraViewModel();

        //    if (!User.IsInRole(RoleConst.Administrador))
        //        return null;

        //    var hasta = (DateTime.Now.AddMonths(-6).AddDays(DateTime.Now.Day * -1));
        //    var series = new Repositorio<Ticket>(db).TraerTodos().Where(x => x.FechaCreacion > hasta)
        //        .GroupBy(x => x.FechaCreacion.Month)
        //        .OrderBy(x => x.Key);

        //    viewModel = new GraficoBarraViewModel();
        //    viewModel.series.Add(new GraficoBarraSeriesViewModel()
        //    {
        //        name = "Evolutivo",
        //        colorByPoint = true,
        //        data = series.Select(x => new GraficoBarraSeriesDataViewModel()
        //        {
        //            name = x.Key.ToString(),
        //            y = x.Count(),
        //            drilldown = x.Key.ToString()
        //        }).ToList()
        //    });

        //    viewModel.drilldown = new GraficoBarraDrilldownViewModel()
        //    {
        //        series = series.Select(x => new GraficoBarraSeriesDrilldownViewModel()
        //        {
        //            name = x.Key.ToString(),
        //            id = x.Key.ToString(),
        //            data = series.SelectMany(y => y)
        //                .Where(y => y.FechaCreacion.Month == x.Key &&
        //                    y.TipoTicket != null)
        //                .GroupBy(y => y.TipoTicket.Nombre)
        //                .OrderBy(y => y.Key)
        //                .Select(y => new GraficoBarraSeriesDrilldownDataViewModel()
        //                {
        //                    nombre = y.Key,
        //                    valor = y.Count()
        //                }).ToList()
        //        }).ToList()
        //    };

        //    return PartialView("~/Views/Home/Partial/_GraficoEvolutivoTicket.cshtml", viewModel);
        //}

        //[HttpGet]
        //public PartialViewResult _ContactarPuntoEntrega()
        //{
        //    var userID = User.Identity.GetUserId();
        //    var UsuarioActual = new Repositorio<Colaborador>(db).TraerTodos().FirstOrDefault(x => x.UsuarioApplicacion.Id == userID);
        //    IQueryable<PuntoVentaAContactarDTO> data;
        //    if (User.IsInRole("Administrador"))
        //        data = Pedido.ObtenerPuntosEntregaAContactar(db, null);
        //    else
        //        data = Pedido.ObtenerPuntosEntregaAContactar(db, UsuarioActual.Id);

        //    var model = new PuntoEntregaContactarViewModel();
            
        //        model.Listado = data.Take(7).ToList();
        //    model.CantidadTotal = data.Count();

        //    return PartialView("~/Views/Home/Partial/_ContactarPuntoEntrega.cshtml", model);
        //}

        //[HttpGet]
        //public PartialViewResult _GeneradorArchivosImportadorBejerman(string fecha = null)
        //{
        //    DateTime? date = fecha.CustomParseNullable();
        //    DateTime valorFecha = date.HasValue ? date.Value : DateTime.Now.AddDays(1);
            
        //    var corteFacturarLunes = new DateTime(valorFecha.Year, valorFecha.Month, valorFecha.Day, 15, 0, 0);

        //    var fechaMinima = valorFecha.AddDays(0).Date;
        //    var fechaMaxima = valorFecha.AddDays(1).Date;

        //    // Este caso es solo para los viernes
        //    if (valorFecha.DayOfWeek == DayOfWeek.Friday &&
        //        valorFecha >= corteFacturarLunes)
        //    {
        //        fechaMinima = fechaMinima.AddDays(2);
        //        fechaMaxima = fechaMaxima.AddDays(2);
        //    }

        //    var pedidos = new Repositorio<Pedido>(db).TraerTodos()
        //        .Where(x => (x.Instancia == InstanciaPedido.Pendiente || x.Instancia == InstanciaPedido.Procesando) &&
        //            x.Articulos.Any() &&
        //            x.FechaProgramada < fechaMaxima &&
        //            x.FechaProgramada >= fechaMinima)
        //        .ToList();

        //    ViewBag.Fecha = fechaMinima.ToShortDateString();

        //    return PartialView("~/Views/Home/Partial/_GeneradorArchivosImportadorBejerman.cshtml", pedidos);
        //}

    }
}