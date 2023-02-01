using ChequePrinting.BusinessLogicLayer.Repository.AuditLogRepository;
using ChequePrinting.BusinessLogicLayer.Repository.ChequeBookPrintRepository;
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
    public class ChequeBookPrintController : BaseController
    {
        private readonly IAuditLogRepository auditLogRepository;
        private readonly ILogger logger;
        private readonly IChequeBookPrintRepository chequeBookPrintRepository;
        public ChequeBookPrintController(IChequeBookPrintRepository _chequeBookPrintRepository, IAuditLogRepository _repository, ILogger _logger)
        {
            this.auditLogRepository = _repository;
            this.logger = _logger;
            this.chequeBookPrintRepository = _chequeBookPrintRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetChequeBookPrintItems(ChequeBookPrintSearchVM seachVM)
        {
            JsonOutput json = new JsonOutput();
            if (JsonAccess.Fails())
            {
                json.message = "SESSION_EXPIRED";
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var reports = chequeBookPrintRepository.GetChequeBookPrintItem(seachVM);
                json.success = true;
                json.result = reports;
                //json.result2 = seachVM.Total;
            }
            catch (Exception ex)
            {
                json.success = false;
                logger.LogError(ex);
                json.message = ex.Message;
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }       
        public ActionResult Print(string cheques)
        {
           
            if (JsonAccess.Fails())
            {
                return RedirectToAction("LogOff", "Account");
            }
            string[] chqArr = cheques.Split(',');
            List<ChequeBookViewModel> list = new List<ChequeBookViewModel>();
            if (cheques!=null)
            {
                foreach (var item in chqArr)
                {
                    var issueId = item.Replace("[", "").Replace("]", "");
                    list.AddRange(chequeBookPrintRepository.GetChequeBooks(issueId));
                    chequeBookPrintRepository.UpdateChequeBookPrintFlag(issueId, JsonAccess.UserCode());
                }
                PrepareDataForPrint(ref list);
                
            }
           
            return View(list);
        }
        private void PrepareDataForPrint(ref List<ChequeBookViewModel> list)
        {
            foreach (var item in list)
            {
                if (item.AccountNo.EndsWith("54") || item.AccountNo.EndsWith("55"))
                {
                    item.Branch.BranchName = "NEGOTIABLE ONLY IN NEPAL";
                }
            }
        }
    }
}