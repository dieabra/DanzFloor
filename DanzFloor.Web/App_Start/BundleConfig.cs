using System.Web;
using System.Web.Optimization;

namespace DanzFloor.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/dropzonescripts").Include(
                     "~/assets/dropzone/dropzone.js"));
            bundles.Add(new ScriptBundle("~/bundles/frontbase").Include(
                     "~/assets/SdP/Frontend/js/jquery/1.12.4/jquery.min.js", 
                     "~/assets/SdP/Frontend/js/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/frontinternal").Include(
                     "~/assets/SdP/Frontend/js/clipboard.min.js",
                     "~/assets/SdP/Frontend/js/OwlCarousel2-2.2.1/dist/owl.carousel.min.js",
                     "~/assets/SdP/Frontend/js/cross.js"));

            bundles.Add(new StyleBundle("~/Content/dropzonescss").Include(
                     "~/assets/dropzone/basic.css",
                     "~/assets/dropzone/dropzone.css"));
            bundles.Add(new StyleBundle("~/Content/frontprojectstrap").Include(
                     "~/assets/SdP/Frontend/styles/projectstrap.css",
                     "~/assets/SdP/Frontend/js/OwlCarousel2-2.2.1/dist/assets/owl.carousel.min.css"));
        }
    }
}
