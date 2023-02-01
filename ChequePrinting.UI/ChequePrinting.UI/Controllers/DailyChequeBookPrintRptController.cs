using ChequePrinting.BusinessLogicLayer.Repository.AuditLogRepository;
using ChequePrinting.BusinessLogicLayer.Repository.ConfigRepository;
using ChequePrinting.BusinessLogicLayer.Repository.DailyChequeBookPrintRptRepository;
using ChequePrinting.BusinessLogicLayer.Repository.LoggerRepository;
using ChequePrinting.BusinessLogicLayer.ViewModel;
using ChequePrinting.UI.Models;
using System;
using System.Web.Mvc;

namespace ChequePrinting.UI.Controllers
{
    public class DailyChequeBookPrintRptController : BaseController
    {
        // GET: DailyChequeBookPrintRpt
        private readonly IDailyChequeBookPrintRptRepository dailyChequeBookPrintRptRepository;
        private readonly ILogger logger;
        private readonly IAuditLogRepository auditLogRepository;
        private readonly IConfigRepository configRepository;
        public DailyChequeBookPrintRptController(IConfigRepository _configRepository, IAuditLogRepository _auditLogRepository,ILogger _logger, IDailyChequeBookPrintRptRepository _dailyChequeBookPrintRptRepository)
        {
            this.dailyChequeBookPrintRptRepository = _dailyChequeBookPrintRptRepository;
            this.logger = _logger;
            this.auditLogRepository = _auditLogRepository;
            this.configRepository = _configRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetBranches()
        {
            JsonOutput json = new JsonOutput();
            if (JsonAccess.Fails())
            {
                json.message = "SESSION_EXPIRED";
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var branches = configRepository.GetBranches();
                json.success = true;
                json.result = branches;
            }
            catch (Exception ex)
            {
                json.success = false;
                logger.LogError(ex);
                json.message = ex.Message;
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetChequeBookPrintItems(DailyChequeBookRptSearchVM searchVM)
        {
            JsonOutput json = new JsonOutput();
            if (JsonAccess.Fails())
            {
                json.message = "SESSION_EXPIRED";
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var reports = dailyChequeBookPrintRptRepository.GetDailyChequePrintReport(searchVM);
                json.success = true;
                json.result = reports;
                json.result2 = searchVM.Total;
                
            }
            catch (Exception ex)
            {
                json.success = false;
                logger.LogError(ex);
                json.message = ex.Message;
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult SendSMS(DailyChequeBookRptSearchVM searchVM)
        {
            JsonOutput json = new JsonOutput();
           
            try
            {
                var reports = dailyChequeBookPrintRptRepository.SendSMS(searchVM);
                json.success = true;
                json.message ="Total " + searchVM.Total + " SMS Sent Successfully! ";
                json.result = reports;
                json.result2 = searchVM.Total;
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
        public JsonResult DestroyAccount(DailyChequeBookRptSearchVM searchVM)
        {
            JsonOutput json = new JsonOutput();
            if (JsonAccess.Fails())
            {
                json.message = "SESSION_EXPIRED";
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var result = dailyChequeBookPrintRptRepository.DestroyAccount(searchVM);
                if (result.Status == "success")
                {
                    json.success = true;
                    auditLogRepository.AddAuditLog(new AuditLogViewModel() { UserId = JsonAccess.GetUser(), ActionPerformed = "Destroy Account (Changed CHQISSUEMASTER)", ActionDesc = "Changed CHQISSUEMASTER table status to D of "+searchVM.AccountNo, Url = Request.Url.ToString() });
                }
                else
                {
                    json.success = false;
                    json.message = result.Message;
                }

            }
            catch (Exception ex)
            {
                json.success = false;
                logger.LogError(ex);
                json.message = ex.Message;
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }

    }

}