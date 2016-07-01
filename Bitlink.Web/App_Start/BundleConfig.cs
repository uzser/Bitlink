using System.Web.Optimization;

namespace Bitlink.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/vendors/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/vendors").Include(
                "~/Scripts/vendors/jquery-{version}.js",
                "~/Scripts/vendors/bootstrap.js",
                "~/Scripts/vendors/angular.js",
                "~/Scripts/vendors/angular-route.js",
                "~/Scripts/vendors/respond.js",
                "~/Scripts/vendors/angular-ui/ui-bootstrap.js",
                "~/Scripts/vendors/angular-ui/ui-bootstrap-tpls.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/spa").IncludeDirectory(
                "~/Scripts/spa/", "*.js", true
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/content/css/bootstrap.css",
                "~/content/css/ui-bootstrap-csp.css",
                "~/content/css/site.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}
