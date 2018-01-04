using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DanzFloor.Web.Startup))]
namespace DanzFloor.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
