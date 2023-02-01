using ChequePrinting.BusinessLogicLayer;
using ChequePrinting.BusinessLogicLayer.Repository.AuditLogRepository;
using ChequePrinting.BusinessLogicLayer.Repository.ChequeBookLogRepository;
using ChequePrinting.BusinessLogicLayer.Repository.Db2DataFetchingRepository;
using ChequePrinting.BusinessLogicLayer.Repository.LoggerRepository;
using ChequePrinting.BusinessLogicLayer.ViewModel;
using ChequePrinting.UI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChequePrinting.UI.Controllers
{
    public class ChequeBookLogsController : BaseController
    {
        private readonly IChequeBookLogRepository chequeBookLogRepository;
        private readonly IDb2DataFetchingRepository db2DataFetchingRepository;
        private readonly IAuditLogRepository auditLogRepository;
        private readonly ILogger logger;
        public ChequeBookLogsController(IDb2DataFetchingRepository _db2DataFetchingRepository, IAuditLogRepository _repository, ILogger _logger, IChequeBookLogRepository _chequeBookLogRepository)
        {
            this.auditLogRepository = _repository;
            this.logger = _logger;
            this.chequeBookLogRepository = _chequeBookLogRepository;
            this.db2DataFetchingRepository = _db2DataFetchingRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult DB2Upload(FileUploadViewModel model)
        {
            JsonOutput json = new JsonOutput();


            if (string.IsNullOrEmpty(model.Document))
            {
                json.success = false;
                json.message = "Select Source File First!!!";
                return Json(json, JsonRequestBehavior.AllowGet);
            }

           
            string filePath = string.Empty;
            if (model.Document != null)
            {
                string path = Server.MapPath("~/Doc/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                
                filePath = path + Path.GetFileName(model.DocumentName);
                string extension = Path.GetExtension(model.DocumentName);
                //model.Document.SaveAs(filePath);


                var newFileName = model.DocumentName.Split('.');
                string fName = newFileName[0];
                string ext = newFileName[1];

                model.DocumentName = fName + "_" + Guid.NewGuid().ToString("N").Substring(0, 1) + "." + ext;
                string bData = model.Document.Substring(model.Document.IndexOf(",") + 1);
                byte[] binaryData = Convert.FromBase64String(bData);
                MemoryStream ms = new MemoryStream(binaryData);
                // write to file
                filePath = filePath + model.DocumentName;
                FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
                file.Close();
                ms.Close();



                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[1] {
                                new DataColumn("Data", typeof(string)),
                });


                string fileData = System.IO.File.ReadAllText(filePath);

                foreach (string row in fileData.Split('\n'))
                {
                    
                        if (!string.IsNullOrEmpty(row))
                    {
                        dt.Rows.Add();
                        int i = 0;

                        foreach (string cell in row.Split('\n'))
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell;
                            i++;
                        }
                    }
                }

              
                try
                {
                    var reports = chequeBookLogRepository.UploadDB2(dt);
                    if (reports == true)
                    {
                        json.success = true;
                        json.message ="Uploaded Sucessfully !";
                        return Json(json, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        json.success = false;
                        json.message = "Uploaded Failed !";
                        return Json(json, JsonRequestBehavior.AllowGet);
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

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetBufferLogs(ChequeBookPrintSearchVM seachVM)
        {
            JsonOutput json = new JsonOutput();
            if (JsonAccess.Fails())
            {
                json.message = "SESSION_EXPIRED";
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var reports = chequeBookLogRepository.GetBufferLogs(seachVM);
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
        public JsonResult DownloadChequeBookLogs()
        {
            JsonOutput json = new JsonOutput();
            if (JsonAccess.Fails())
            {
                json.message = "SESSION_EXPIRED";
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            try
            {
                string lastTimeStamp = chequeBookLogRepository.GetLastTimeStamp();
                if (lastTimeStamp == string.Empty)
                {
                    json.success = false;
                    json.message = "System not properly configured. Please contact System Administrator";
                }
                else
                {
                    //step 1: connecting to db2...
                    DataSet ds = db2DataFetchingRepository.GetDataFromDB2(Helper.GetDate(lastTimeStamp));

                    if (Helper.HasRowsInDataSetTable(ds))
                    {
                        //step 2: DownloadToBufferLogs
                        if (chequeBookLogRepository.DownloadToBufferLogs(ds.Tables[0]))
                        {
                            json.success = true;
                            json.message = "Logs download completed.";
                            auditLogRepository.AddAuditLog(new AuditLogViewModel() { UserId = JsonAccess.GetUser().ToString(), ActionPerformed = "Log data Download", ActionDesc = "Logs download completed.", Url = Request.Url.ToString() });
                        }
                        else
                        {
                            json.success = false;
                            json.message = "No Cheque Book Logs found to process";
                        }
                    }
                    else
                    {
                        json.success = false;
                        json.message = "No Cheque Book Logs found to process.";
                    }
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

        [HttpPost]
        public JsonResult ClearDownload()
        {
            JsonOutput json = new JsonOutput();
            if (JsonAccess.Fails())
            {
                json.message = "SESSION_EXPIRED";
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var response = chequeBookLogRepository.ClearBuffer(JsonAccess.GetUser());
                if (response.Status == "success")
                {
                    json.success = true;
                    auditLogRepository.AddAuditLog(new AuditLogViewModel() { UserId = JsonAccess.GetUser().ToString(), ActionPerformed = "Clear Download", ActionDesc = "Buffer data cleared", Url = Request.Url.ToString() });
                }
                else
                {
                    json.success = false;
                    json.message = response.Message;
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

        [HttpPost]
        public JsonResult PostBatch()
        {
            JsonOutput json = new JsonOutput();
            if (JsonAccess.Fails())
            {
                json.message = "SESSION_EXPIRED";
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var response = chequeBookLogRepository.PostBufferChequeLogs();
                if (response.Status == "success")
                {
                    json.success = true;
                    auditLogRepository.AddAuditLog(new AuditLogViewModel() { UserId = JsonAccess.GetUser().ToString(), ActionPerformed = "Post BufferCheque Logs", ActionDesc = "Buffer Cheque logs posted successfully.", Url = Request.Url.ToString() });
                }
                else
                {
                    json.success = false;
                    json.message = response.Message;
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