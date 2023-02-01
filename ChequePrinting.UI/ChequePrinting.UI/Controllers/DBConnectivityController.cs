using ChequePrinting.BusinessLogicLayer;
using ChequePrinting.BusinessLogicLayer.ViewModel;
using ChequePrinting.UI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace ChequePrinting.UI.Controllers
{
    public class DBConnectivityController : Controller
    {
        // GET: DBConnectivity
        public ActionResult Index()
        {
            if (!JsonAccess.Fails())
            {
                return RedirectToAction("LogOff", "Account");
            }

            return View();
        }

        [HttpPost]
        public JsonResult SaveDbConnecitivityInfo(DBConnectivityViewModel model)
        {
            JsonOutput json = new JsonOutput();
            try
            {
                Configuration connectionConfiguration = WebConfigurationManager.OpenWebConfiguration("~");
                var conValue = connectionConfiguration.ConnectionStrings.ConnectionStrings["myconnection"];

                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("data source=" + model.DataSource);
                if (!string.IsNullOrEmpty(model.Port))
                {
                    sb.AppendFormat("," + model.Port);
                }
                sb.AppendFormat(";");
                sb.AppendFormat(" initial catalog=" + model.Database + ";");
                sb.AppendFormat(" User Id=" + model.UserName + ";");
                string password = Helper.Encrypt(model.Password);
                sb.AppendFormat(" Password=" + password + ";");
                sb.AppendFormat(" MultipleActiveResultSets=True;");

                conValue.ConnectionString = sb.ToString();
                connectionConfiguration.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");

                json.message = "Connection string information saved successfully";
                json.success = true;
            }
            catch (Exception ex)
            {
                json.message = ex.Message.ToString();
                json.success = false;
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}