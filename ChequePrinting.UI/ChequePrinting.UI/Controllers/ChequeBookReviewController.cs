using ChequePrinting.BusinessLogicLayer;
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
    public class ChequeBookReviewController : BaseController
    {
        private readonly IAuditLogRepository auditLogRepository;
        private readonly ILogger logger;
        private readonly IChequeBookPrintRepository chequeBookPrintRepository;
        public ChequeBookReviewController(IChequeBookPrintRepository _chequeBookPrintRepository, IAuditLogRepository _repository, ILogger _logger)
        {
            this.auditLogRepository = _repository;
            this.logger = _logger;
            this.chequeBookPrintRepository = _chequeBookPrintRepository;
        }
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult UpdateCheque(string[] checkedItem, string NoOfLeaves, string ChequeStartNo)
        {
            JsonOutput json = new JsonOutput();
            ChequeBookViewModel model = new ChequeBookViewModel();
            model.IssueID = Helper.GetInteger(checkedItem.FirstOrDefault());
            model.NoOfLeaves = Helper.GetDBNUllString(NoOfLeaves);
            model.ChequeStartNo = Helper.GetDBNUllString(ChequeStartNo);

            model.UpdatedBy = JsonAccess.GetUser().ToString();
            var response = chequeBookPrintRepository.UpdateCheque(model);
            if (response.Status == "failed")
            {
                json.success = false;
                logger.LogError(response.Exception);
                json.message = response.Message;
            }
            else
            {
                json.success = true;
            }
            json.result = response.Message;
            json.result2 = response.LastId;
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RevieweCheque(string[] checkedItem)
        {
            JsonOutput json = new JsonOutput();
            ChequeBookViewModel model = new ChequeBookViewModel();
          
            if (checkedItem != null)
            {
                foreach (var item in checkedItem)
                {
                    //var issueId = item.Replace("[", "").Replace("]", "");
                    var issueId = item;
                    model.IssueID = Helper.GetInteger(issueId);
                    model.UpdatedBy = JsonAccess.GetUser().ToString();
                    var response = chequeBookPrintRepository.ReviewCheque(model);
                    if (response.Status == "failed")
                    {
                        json.success = false;
                        logger.LogError(response.Exception);
                        json.message = response.Message;
                        return Json(json, JsonRequestBehavior.AllowGet);
                    }
                }

                json.success = true;
                json.result = "Updated Sucessfully!";
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {
                json.success = false;
                json.message = "Please Select Reviewed Items";
                return Json(json, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ReviewAll(string[] checkedItem)
        {
            JsonOutput json = new JsonOutput();
            ChequeBookViewModel model = new ChequeBookViewModel();
            //model.IssueID = Helper.GetInteger(checkedItem.FirstOrDefault());
            model.IssueID = Helper.GetInteger(checkedItem);
            model.UpdatedBy = JsonAccess.GetUser().ToString();
            var response = chequeBookPrintRepository.ReviewAll(model);
            if (response.Status == "failed")
            {
                json.success = false;
                logger.LogError(response.Exception);
                json.message = response.Message;
            }
            else
            {
                json.success = true;
            }
            json.result = response.Message;
            json.result2 = response.LastId;
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateRequisition(string[] checkedItem, string NoOfLeaves, string ChequeStartNo)
        {
            JsonOutput json = new JsonOutput();
            ChequeBookViewModel model = new ChequeBookViewModel();
            model.IssueID = Helper.GetInteger(checkedItem.FirstOrDefault());
            model.NoOfLeaves = Helper.GetDBNUllString(NoOfLeaves);
            model.ChequeStartNo = Helper.GetDBNUllString(ChequeStartNo);

            model.UpdatedBy = JsonAccess.GetUser().ToString();
            var response = chequeBookPrintRepository.UpdateRequisition(model);
            if (response.Status == "failed")
            {
                json.success = false;
                logger.LogError(response.Exception);
                json.message = response.Message;
            }
            else
            {
                json.success = true;
            }
            json.result = response.Message;
            json.result2 = response.LastId;
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UndoCheque(string[] checkedItem)
        {
            JsonOutput json = new JsonOutput();
            ChequeBookViewModel model = new ChequeBookViewModel();
            model.IssueID = Helper.GetInteger(checkedItem.FirstOrDefault());
            model.UpdatedBy = JsonAccess.GetUser().ToString();
            var response = chequeBookPrintRepository.UndoCheque(model);
            if (response.Status == "failed")
            {
                json.success = false;
                logger.LogError(response.Exception);
                json.message = response.Message;
            }
            else
            {
                json.success = true;
            }
            json.result = response.Message;
            json.result2 = response.LastId;
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult UndoRequisition(string[] checkedItem)
        {
            JsonOutput json = new JsonOutput();
            ChequeBookViewModel model = new ChequeBookViewModel();
            model.IssueID = Helper.GetInteger(checkedItem.FirstOrDefault());
            model.UpdatedBy = JsonAccess.GetUser().ToString();
            var response = chequeBookPrintRepository.UndoRequisition(model);
            if (response.Status == "failed")
            {
                json.success = false;
                logger.LogError(response.Exception);
                json.message = response.Message;
            }
            else
            {
                json.success = true;
            }
            json.result = response.Message;
            json.result2 = response.LastId;
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetChequeBookReview(ChequeBookPrintSearchVM seachVM)
        {
            JsonOutput json = new JsonOutput();
            try
            {
                var reports = chequeBookPrintRepository.GetChequeBookReview(seachVM);
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

           
            string[] chqArr = cheques.Split(',');
            List<ChequeBookViewModel> list = new List<ChequeBookViewModel>();
            if (cheques != null)
            {
                foreach (var item in chqArr)
                {
                    var issueId = item.Replace("[", "").Replace("]", "");
                    list.AddRange(chequeBookPrintRepository.GetChequeReview(issueId));
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