using System.Web;
using System.Web.Optimization;

namespace Eduegate.ERP.School.ParentPortal
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
               "~/Scripts/jquery-{version}.js")
               .Include("~/Scripts/jquery.prettyPhoto.js")
               .Include("~/Scripts/draggable/draggable.bundle.min.js")
               );

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                       "~/Scripts/jquery.validate.js")
                       .Include("~/Scripts/jquery.validate.unobtrusive.js")
                       .Include("~/Scripts/jquery.validate.unobtrusive.custom.js")
                       .Include("~/app/CustomValidation.js")
                       .Include("~/Scripts/jquery.signalR-2.2.2.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/font-awesome.min.css",
                      "~/Content/bootstrap.css",
                      "~/Content/normalize.css",
                      "~/Content/materialize.min.css",
                       "~/Content/c3.css",
                      "~/Content/main.css")
                );

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                 "~/Scripts/angular.js",
                 "~/Scripts/angular-route.js",
                 "~/Scripts/angular-animate.js",
                 "~/Scripts/angular-sanitize.js",
                 "~/Scripts/i18n/angular-locale_en.js",
                  "~/Scripts/tmhDynamicLocale.js",
                  "~/Scripts/angular-filter.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
                  "~/Scripts/jquery.waypoints.min.js",
                  "~/Scripts/moment.min.js",
                   "~/Scripts/angular-moment.min.js",
                  "~/Scripts/daterangepicker.js",
                  "~/Scripts/datetimepicker.js",
                  "~/Scripts/Select.js",
                  "~/Scripts/jquery-ui.js",
                  "~/Scripts/jquery.ui.touch-punch.min.js",
                  "~/Scripts/Custom.js",
                  "~/Scripts/dropzone.js",
                  "~/Scripts/zebra_datepicker.js",
                  "~/Scripts/date.js",
                  "~/Scripts/excanvas.js",
                  "~/Scripts/jquery.signature.js",
                  "~/Scripts/interact.js",
                  "~/Scripts/rrule/rrule.js",
                  "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                  "~/Scripts/boostrapcolorpicker/bootstrap-colorpicker-module.js",
                  "~/Scripts/calendar/angular-bootstrap-calendar.js", 
                  "~/Scripts/d3.js",
                  "~/Scripts/c3.js",
                  "~/Scripts/underscore-min.js",
                  "~/Scripts/shortcut.js"
                  ));

            bundles.Add(new ScriptBundle("~/bundles/appmodels")
            .IncludeDirectory("~/app", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/others")
              .Include("~/app/utility.js")
               .Include("~/app/chartbuilder.js")
              .Include("~/Scripts/JSLINQ.js")
               .Include("~/app/BrowserNotifier.js")
               .Include("~/Scripts/materialize.min.js")
              );

            bundles.Add(new ScriptBundle("~/bundles/jslinq")
            .Include("~/Scripts/JSLINQ.js"));

            bundles.Add(new ScriptBundle("~/bundles/highchart")
          .Include("~/Scripts/highcharts/charts-ng.js")
          .Include("~/Scripts/highcharts/highstock.src.js")
          .Include("~/Scripts/toaster.js"));
        }
    }
}
