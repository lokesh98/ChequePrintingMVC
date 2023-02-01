using ChequePrinting.BusinessLogicLayer;
using ChequePrinting.BusinessLogicLayer.Repository.AuditLogRepository;
using ChequePrinting.BusinessLogicLayer.Repository.SecurityMatrixRepository;
using ChequePrinting.BusinessLogicLayer.ViewModel;
using ChequePrinting.UI.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ChequePrinting.UI.Controllers
{
    public class SecurityMatrixController : BaseController
    {
        private readonly ISecurityMatrixRepository repository;
        private readonly IAuditLogRepository auditLogRepository;
        public SecurityMatrixController(IAuditLogRepository _auditLogRepository, ISecurityMatrixRepository _repo)
        {
            this.auditLogRepository = _auditLogRepository;
            this.repository = _repo;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetSecurityMatrixList(SecuritySearchVM searchVM)
        {
            JsonOutput json = new JsonOutput();
            if (JsonAccess.Fails())
            {
                json.message = "SESSION_EXPIRED";
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var securityMetrics = repository.GetUserSecurityMetricsReport(searchVM);
                json.success = true;
                json.result = securityMetrics;
                json.result2 = searchVM.Total;
            }
            catch (Exception ex)
            {
                json.success = false;
                json.message = ex.Message;
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ExportToExcel(SecuritySearchVM searchVM)
        {
            JsonOutput json = new JsonOutput();
            searchVM.CurrentPage = 1;
            searchVM.PerPage *= searchVM.Total;
            var securityMetrics = repository.GetUserSecurityMetricsReport(searchVM);
            var dt = Utility.ToDataTable<SecurityMetricsViewModel>(securityMetrics.AsQueryable());
            Session["ExcelReport"] = Utility.ExportToExcel(dt);
            json.success = true;
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Download()
        {
            if (Session["ExcelReport"] != null)
            {
                byte[] data = Session["ExcelReport"] as byte[];
                Session["ExcelReport"] = null;
                string fileName = "SecurityMatrix_" + DateTime.Now.ToString(Settings.LongDateFormat) + ".xlsx";
                auditLogRepository.AddAuditLog(new AuditLogViewModel() { UserId = JsonAccess.GetUser(), ActionPerformed = "Download", ActionDesc = "Security Matrix Report Downloaded", Url = Request.Url.ToString() });
                return File(data, "application/octet-stream", fileName);
            }
            else
            {
                return new EmptyResult();
            }
        }
    }
}