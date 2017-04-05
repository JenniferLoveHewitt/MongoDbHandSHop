using System.Web;
using System.Web.Optimization;

namespace MongoDbMarket.WebUI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundle)
        {
            bundle.UseCdn = true;

            var jQueryPath = "https://code.jquery.com/jquery-3.1.1.min.js";

            bundle.Add(new ScriptBundle("~/bundles/jquery", jQueryPath).
                Include("~/Scripts/jquery-{version}.js"));

            bundle.Add(new ScriptBundle("~/bundles/uploadScript").
                Include("~/Scripts/uploadScript.js"));
        }
    }
}