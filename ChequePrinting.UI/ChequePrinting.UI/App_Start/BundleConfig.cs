using System.Web;
using System.Web.Optimization;

namespace ChequePrinting.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js").Include(
                        "~/Scripts/jquery.growl.js").Include(
                        "~/Scripts/bootstrap-datepicker.js")
                        );

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/vuejs").Include(
                      "~/Scripts/Vuejs/vue.js").Include(
                      "~/Scripts/Vuejs/vue-resource.min.js")
                      .Include(
                      "~/Scripts/Vuejs/vuejs_paginate.js"));

            bundles.Add(new ScriptBundle("~/bundles/blockui").Include(
                   "~/Scripts/blockUI.js").Include(
                   "~/Scripts/Custom/CustomerScript.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                        "~/Content/font-awesome.min.css",
                       "~/Content/bootstrap-datepicker.css",
                      "~/Content/CustomCss/general.css",
                       "~/Content/CustomCss/MICR.css",
                      "~/Content/CustomCss/customnavigation.css",
                      "~/Scripts/jquery.growl.css"
                      ));
            bundles.Add(new StyleBundle("~/Content/blockuiCss").Include(
                     "~/Scripts/Custom/blockui.css"
                     ));
        }
    }
}
