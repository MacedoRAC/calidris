using System.Web;
using System.Web.Optimization;

namespace FirstREST
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/myjs").Include(
                        "~/Scripts/jquery-2.1.1.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/plugins/metisMenu/jquery.metisMenu.js",
                        "~/Scripts/plugins/slimscroll/jquery.slimscroll.js",
                        "~/Scripts/inspinia.js",
                        "~/Scripts/plugins/pace/pace.js",
                        "~/Scripts/plugins/datepicker/bootstrap-datepicker.js",
                        "~/Scripts/plugins/footable/footable.js",
                        "~/Scripts/myScript.js"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new StyleBundle("~/Content/mycss").Include(
                        "~/Content/css/bootstrap.css",
                        "~/Content/font-awesome/css/font-awesome.css",
                        "~/Content/css/plugins/footable/footable.core.css",
                        "~/Content/css/animate.css",
                        "~/Content/css/style.css",
                        "~/Content/css/plugins/datapicker/datepicker3.css"));
        }
    }
}