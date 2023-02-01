using ChequePrinting.BusinessLogicLayer.Repository.AuditLogRepository;
using ChequePrinting.BusinessLogicLayer.Repository.ChequeBookPrintRepository;
using ChequePrinting.BusinessLogicLayer.Repository.LoggerRepository;
using ChequePrinting.BusinessLogicLayer.Repository.RequisitionRepository;
using ChequePrinting.BusinessLogicLayer.ViewModel;
using ChequePrinting.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChequePrinting.UI.Controllers
{
    public class RequisitionFormController : BaseController
    {
        private readonly IAuditLogRepository auditLogRepository;
        private readonly IChequeBookPrintRepository chequeBookPrintRepository;
        private readonly IRequisitionFormRepository requisitionFormRepository;
        private readonly ILogger logger;
        public RequisitionFormController(IRequisitionFormRepository _requisitionFormRepository, IAuditLogRepository _auditLogRepository, ILogger _logger, IChequeBookPrintRepository _chequeBookPrintRepository)
        {
            this.auditLogRepository = _auditLogRepository;
            this.logger = _logger;
            this.chequeBookPrintRepository = _chequeBookPrintRepository;
            this.requisitionFormRepository = _requisitionFormRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetRequisitionItems(ChequeBookPrintSearchVM seachVM)
        {
            JsonOutput json = new JsonOutput();
            if (JsonAccess.Fails())
            {
                json.message = "SESSION_EXPIRED";
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var reports = chequeBookPrintRepository.GetRequisitionPrintItem(seachVM);
                json.success = true;
                json.result = reports;
                json.result2 = seachVM.Total;
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
        public ActionResult PrintRequisition(string requistion, bool printLabel,bool byPass)
        {
            if (JsonAccess.Fails())
            {
                return RedirectToAction("LogOff", "Account");
            }
            string[] chqArr = requistion.Split(',');
            List<ChequeBookViewModel> list = new List<ChequeBookViewModel>();
            if (requistion != null)
            {
                foreach (var item in chqArr)
                {
                    list.AddRange(requisitionFormRepository.GetReq(item));
                    if (!printLabel)
                    {
                        requisitionFormRepository.UpdateRequisitionFormPrintFlag(item, JsonAccess.UserCode());
                    }
                }
                if (!byPass)
                {
                    if (!printLabel)
                    {
                        //nothing to do
                        PrintRequisitionForm(ref list);
                    }
                    else
                    {
                        //to do
                        GenerateCustomerRecord(ref list);
                    }
                }                
            }
            return View("_PrintRequisition",list);
        }
        private void PrintRequisitionForm(ref List<ChequeBookViewModel> list)
        {
            
        }
        private void GenerateCustomerRecord(ref List<ChequeBookViewModel> list)
        {
            foreach (var item in list)
            {
                string accno = item.AccountNo;
                string leftaccno = accno.Substring(0, 7);
                string midaccno = "XXXXX";
                string rightaccno = accno.Substring(12);
                item.AccountNo = leftaccno + midaccno + rightaccno;
                //dr3[8] = drRecords["CHQSTARTNO"].ToString() + " to " + (Convert.ToInt64(drRecords["CHQSTARTNO"].ToString()) + Convert.ToInt64(drRecords["NOOFLEAVES"].ToString()) - 1).ToString();
                // dr3[9] = (Convert.ToInt64(drRecords["CHQSTARTNO"].ToString()) + Convert.ToInt64(drRecords["NOOFLEAVES"].ToString()) - 1).ToString();
            }
        }
    }
}