using ChequePrinting.BusinessLogicLayer;
using ChequePrinting.BusinessLogicLayer.Repository.AuditLogRepository;
using ChequePrinting.BusinessLogicLayer.Repository.LoggerRepository;
using ChequePrinting.BusinessLogicLayer.ViewModel;
using ChequePrinting.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChequePrinting.UI.Controllers
{
    public class AuditLogController : BaseController
    {
        private readonly IAuditLogRepository auditLogRepository;
        private readonly ILogger logger;
        public AuditLogController(IAuditLogRepository _repository, ILogger _logger)
        {
            this.auditLogRepository = _repository;
            this.logger = _logger;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAuditLogs(AuditSearchVM model)
        {
            JsonOutput json = new JsonOutput();
            try
            {
                var auditLogs = auditLogRepository.GetAuditLogs(model);
                json.success = true;
                json.result = auditLogs;
                json.result2 = model.Total;
            }
            catch (Exception ex)
            {
                json.success = false;
                logger.LogError(ex);
                json.message = ex.Message;
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ExportToExcel(AuditSearchVM model)
        {
            JsonOutput json = new JsonOutput();
            model.PerPage *= model.Total;
            var auditLogs = auditLogRepository.GetAuditLogs(model);
            var dt = Utility.GetAuditLogRptDT(auditLogs.AsQueryable());
            Session["AuditLogRpt"] = Utility.ExportToExcel(dt);
            json.success = true;
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Download()
        {
            if (Session["AuditLogRpt"] != null)
            {
                byte[] data = Session["AuditLogRpt"] as byte[];
                Session["AuditLogRpt"] = null;
                string fileName = "AuditLogReport_" + DateTime.Now.ToString(Settings.LongDateFormat) + ".xlsx";
                auditLogRepository.AddAuditLog(new AuditLogViewModel() { UserId = JsonAccess.GetUser().ToString(), ActionPerformed = "Download", Url = Request.Url.ToString() });
                return File(data, "application/octet-stream", fileName);
            }
            else
            {
                return new EmptyResult();
            }
        }
    }
}