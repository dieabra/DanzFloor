using System.Web.Mvc;
using System.Web.Routing;

namespace DanzFloor.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Google API Sign-in",
                url: "signin-google",
                defaults: new { controller = "Cuenta", action = "ReturnFromGoogle" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Inicio", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
