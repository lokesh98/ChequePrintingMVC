using ChequePrinting.BusinessLogicLayer;
using ChequePrinting.BusinessLogicLayer.Repository.AuditLogRepository;
using ChequePrinting.BusinessLogicLayer.Repository.ConfigRepository;
using ChequePrinting.BusinessLogicLayer.Repository.LoggerRepository;
using ChequePrinting.BusinessLogicLayer.Repository.UserRepository;
using ChequePrinting.BusinessLogicLayer.ViewModel;
using ChequePrinting.UI.Models;
using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace ChequePrinting.UI.Controllers
{
    public class UserController : BaseController
    {
        private readonly ILogger logger;
        private readonly IUserRepository userRepository;
        private readonly IConfigRepository configRepository;
        private readonly IAuditLogRepository auditLogRepository;
        public UserController(IAuditLogRepository _auditLogRepository, IUserRepository _userRepository, IConfigRepository _configRepository, ILogger _logger)
        {
            this.userRepository = _userRepository;
            this.configRepository = _configRepository;
            this.logger = _logger;
            this.auditLogRepository = _auditLogRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetMasterData()
        {
            JsonOutput json = new JsonOutput();
            if (JsonAccess.Fails())
            {
                json.message = "SESSION_EXPIRED";
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var userRoles = configRepository.GetUserRoles();
                var userStatus = configRepository.GetUserStatus();
                json.success = true;
                json.result = userRoles;
                json.result2 = userStatus;
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
        public JsonResult GetUsers(UserSearchVM model)
        {
            JsonOutput json = new JsonOutput();
            if (JsonAccess.Fails())
            {
                json.message = "SESSION_EXPIRED";
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var users = userRepository.GetUserList(model);
                json.result = users;
                json.result2 = model.Total;
                json.success = true;
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
        public JsonResult AddNewUser(UserViewModel model)
        {
            JsonOutput json = new JsonOutput();
            if (JsonAccess.Fails())
            {
                json.message = "SESSION_EXPIRED";
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            model.CreatedBy = JsonAccess.GetUser().ToString();
            var response = userRepository.AddNewUser(model);
            if (response.Status == "failed")
            {
                json.success = false;
                logger.LogError(response.Exception);
                json.message = response.Message;
            }
            else
            {
                json.success = true;
                auditLogRepository.AddAuditLog(new AuditLogViewModel() { UserId = JsonAccess.GetUser(), ActionPerformed = "Created User", ActionDesc = "Created New User :" + model.UserName + " successfully.", Url = Request.Url.ToString() });
            }
            json.result = response.Message;
            json.result2 = response.LastId;
            return Json(json, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult UpdateUser(UserViewModel model)
        {
            JsonOutput json = new JsonOutput();
            if (JsonAccess.Fails())
            {
                json.message = "SESSION_EXPIRED";
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            model.ModifiedBy = JsonAccess.GetUser().ToString();
            var prvModel = userRepository.GetUserByUserID(model.UserCode);
            var response = userRepository.UpdateUser(model);
            json.success = true;
            if (response.Status == "failed")
            {
                json.success = false;
                logger.LogError(response.Exception);
                json.message = response.Message;
            }
            else
            {
                SaveUserUpdateLog(prvModel, model);
            }
            json.result = response.Message;
            json.result2 = response.LastId;
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteUser(string userId)
        {
            JsonOutput json = new JsonOutput();
            if (JsonAccess.Fails())
            {
                json.message = "SESSION_EXPIRED";
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            var response = userRepository.DeleteUser(userId);
            if (response.Status == "failed")
            {
                json.success = false;
                logger.LogError(response.Exception);
                json.message = response.Message;
            }
            else
            {
                json.success = true;
                auditLogRepository.AddAuditLog(new AuditLogViewModel() { UserId = JsonAccess.GetUser(), ActionDesc = "Deleted User with User ID: " + userId, ActionPerformed = "Deleted User", Url = Request.Url.ToString() });
            }
            json.result = response.Message;
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ExportToExcel(UserSearchVM model)
        {
            JsonOutput json = new JsonOutput();
            model.PerPage *= model.Total;

            var users = userRepository.GetUserList(model);
            var userRpt = users.Select(x => new UserReportViewModel()
            {
                UserCode = x.UserCode,
                UserName = x.UserName,
                RoleName = x.RoleName,
                CanDownload = x.CanDownload,
                CanUndoPrint = x.CanUndoPrint,
                IsLoggedIn = Helper.GetDBNUllBoolean(x.IsLogin.ToString()),
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                Email = x.Email,
                LastLogin = x.LastLogin,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate,
                Status = x.StatusName
            });


            DataTable dt = Utility.ToDataTable<UserReportViewModel>(userRpt.AsQueryable());
            Session["ExcelReport"] = Helper.ExportToExcel(dt);
            json.success = true;
            auditLogRepository.AddAuditLog(new AuditLogViewModel() { UserId = JsonAccess.GetUser(), ActionDesc = "Download Userlist", ActionPerformed = "User list report downloaded", Url = Request.Url.ToString() });
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Download()
        {
            if (Session["ExcelReport"] != null)
            {
                byte[] data = Session["ExcelReport"] as byte[];
                Session["ExcelReport"] = null;
                string fileName = "UserList_" + DateTime.Now.ToString(Settings.LongDateFormat) + ".xlsx";
                return File(data, "application/octet-stream", fileName);
            }
            else
            {
                return new EmptyResult();
            }
        }

        private void SaveUserUpdateLog(UserViewModel prvModel, UserViewModel model)
        {
            if (!prvModel.Email.Equals(model.Email))
            {
                auditLogRepository.AddAuditLog(new AuditLogViewModel() { UserId = JsonAccess.GetUser(), ActionPerformed = "Update", ActionDesc = "Updated User " + model.UserCode + " with Email= " + model.Email + ", from " + prvModel.Email, OldValue = prvModel.Email, NewValue = model.Email, Url = Request.Url.ToString(), ReflectedUserID = model.UserCode });
            }
            if (!prvModel.StatusId.Equals(model.StatusId))
            {
                string newStatus = configRepository.GetUserStatus().Where(x => x.StatusId == model.StatusId).FirstOrDefault().StatusName;
                auditLogRepository.AddAuditLog(new AuditLogViewModel() { UserId = JsonAccess.GetUser(), ActionPerformed = "Update", ActionDesc = "Updated User " + model.UserCode + " with Status= " + newStatus + ", from " + prvModel.StatusName, OldValue = prvModel.StatusName, NewValue = newStatus, Url = Request.Url.ToString(), ReflectedUserID = model.UserCode });
            }

            if (!prvModel.IsLocked.Equals(model.IsLocked))
            {
                string newIsLocked = model.IsLocked.ToString();
                string oldIsLocked = prvModel.IsLocked.ToString();
                if (oldIsLocked.Equals("False")|| oldIsLocked.Equals("false")) { oldIsLocked = "UnLocked"; }
                else { oldIsLocked = "Locked"; }
                if (newIsLocked.Equals("False")|| newIsLocked.Equals("false")) { newIsLocked = "UnLocked"; }
                else { newIsLocked = "Locked"; }
              
                auditLogRepository.AddAuditLog(new AuditLogViewModel() { UserId = JsonAccess.GetUser(), ActionPerformed = "Update", ActionDesc = "Updated User " + model.UserCode + "  with Role= " + newIsLocked + ", from " + oldIsLocked, OldValue = oldIsLocked, NewValue = newIsLocked, Url = Request.Url.ToString(), ReflectedUserID = model.UserCode });

            }
            
            if (!prvModel.IsLogin.Equals(model.IsLogin))
            {
                string oldIsLogin = model.IsLogin.ToString();
                string newIsLogin = prvModel.IsLogin.ToString();
                if (oldIsLogin.Equals("0")) { oldIsLogin = "LogOut"; }
                else { oldIsLogin = "Login"; }
                if (newIsLogin.Equals("0")) { newIsLogin = "LogOut"; }
                else  { newIsLogin = "Login";}
                
                auditLogRepository.AddAuditLog(new AuditLogViewModel() { UserId = JsonAccess.GetUser(), ActionPerformed = "Update", ActionDesc = "Updated User " + model.UserCode + " with Login Status= " + newIsLogin + ", from " + oldIsLogin, OldValue = oldIsLogin, NewValue = newIsLogin, Url = Request.Url.ToString(), ReflectedUserID = model.UserCode });
            }
            if (!prvModel.UserName.Equals(model.UserName))
            {
                auditLogRepository.AddAuditLog(new AuditLogViewModel() { UserId = JsonAccess.GetUser(), ActionPerformed = "Update", ActionDesc = "Updated User " + model.UserCode + " with UserName= " + model.UserName + ", from " + prvModel.UserName, OldValue = prvModel.UserName, NewValue = model.UserName, Url = Request.Url.ToString(), ReflectedUserID = model.UserCode });
            }
            if (!prvModel.RoleId.Equals(model.RoleId))
            {
                string newRole = configRepository.GetUserRoles().Where(x => x.RoleId == model.RoleId).FirstOrDefault().RoleName;
                //auditLogRepository.AddAuditLog(new AuditLogViewModel() { UserId = JsonAccess.GetUser(), ActionPerformed = "Update", ActionDesc = "Updated User " + model.UserCode + "  with Role= " + newRole + ", from " + prvModel.RoleName, OldValue = prvModel.RoleName, NewValue = model.RoleName, Url = Request.Url.ToString(), ReflectedUserID = model.UserCode });
                auditLogRepository.AddAuditLog(new AuditLogViewModel() { UserId = JsonAccess.GetUser(), ActionPerformed = "Update", ActionDesc = "Updated User " + model.UserCode + "  with Role= " + newRole + ", from " + prvModel.RoleName, OldValue = prvModel.RoleName, NewValue = newRole, Url = Request.Url.ToString(), ReflectedUserID = model.UserCode });

            }
            if (!prvModel.CanDownload.Equals(model.CanDownload))
            {
                auditLogRepository.AddAuditLog(new AuditLogViewModel() { UserId = JsonAccess.GetUser(), ActionPerformed = "Update", ActionDesc = "Updated User " + model.UserCode + "  with Can Download= " + model.CanDownload + ", from " + prvModel.CanDownload, OldValue = prvModel.CanDownload.ToString(), NewValue = model.CanDownload.ToString(), Url = Request.Url.ToString(), ReflectedUserID = model.UserCode });
            }
            if (!prvModel.CanUndoPrint.Equals(model.CanUndoPrint))
            {
                auditLogRepository.AddAuditLog(new AuditLogViewModel() { UserId = JsonAccess.GetUser(), ActionPerformed = "Update", ActionDesc = "Updated User " + model.UserCode + "  with Can Undo Print= " + model.CanUndoPrint + ", from " + prvModel.CanUndoPrint, OldValue = prvModel.CanUndoPrint.ToString(), NewValue = model.CanUndoPrint.ToString(), Url = Request.Url.ToString(), ReflectedUserID = model.UserCode });
            }
        }
    }

}