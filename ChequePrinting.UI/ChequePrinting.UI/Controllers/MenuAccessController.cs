using ChequePrinting.BusinessLogicLayer.Repository.AuditLogRepository;
using ChequePrinting.BusinessLogicLayer.Repository.ConfigRepository;
using ChequePrinting.BusinessLogicLayer.Repository.LoggerRepository;
using ChequePrinting.BusinessLogicLayer.Repository.MenuRepository;
using ChequePrinting.BusinessLogicLayer.ViewModel;
using ChequePrinting.UI.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ChequePrinting.UI.Controllers
{
    public class MenuAccessController : BaseController
    {
        private readonly IMenuRepository menuRepository;
        private readonly IConfigRepository configRepository;
        private readonly IAuditLogRepository auditLogRepository;
        private readonly ILogger logger;
        public MenuAccessController(IMenuRepository _menuRepository, IConfigRepository _configRepository, ILogger _logger, IAuditLogRepository _auditLogRepository)
        {
            this.menuRepository = _menuRepository;
            this.configRepository = _configRepository;
            this.logger = _logger;
            this.auditLogRepository = _auditLogRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetUserRoles()
        {
            JsonOutput json = new JsonOutput();
            if (JsonAccess.Fails())
            {
                json.message = "SESSION_EXPIRED";
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var role = configRepository.GetUserRoles();

                json.result = role;
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

        [HttpGet]
        public JsonResult GetMenuItems(string userGroup)
        {
            JsonOutput json = new JsonOutput();
            if (JsonAccess.Fails())
            {
                json.message = "SESSION_EXPIRED";
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var menus = menuRepository.GetMenuForAccessMgmt();
                var mappings = menuRepository.GetMenuMapping(userGroup);

                json.result = menus;
                json.result2 = mappings;

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
        public JsonResult SaveMenuMapping(List<MenuViewModel> menuItems, string userGroup)
        {
            JsonOutput json = new JsonOutput();
            if (JsonAccess.Fails())
            {
                json.message = "SESSION_EXPIRED";
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            menuRepository.DeleteMenuAccess(userGroup);

            ResponseViewModel response = new ResponseViewModel();
            foreach (var item in menuItems)
            {
                var model = new MenuAccessViewModel()
                {
                    RoleDesc = userGroup,
                    MenuId = item.MenuId,
                    MappingId = 0,
                };
                response = menuRepository.SaveRoleMenuMapping(model);
            }

            if (response.Status == "failed")
            {
                json.success = false;
                logger.LogError(response.Exception);
                json.message = response.Message;
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {
                auditLogRepository.AddAuditLog(new AuditLogViewModel() { UserId = JsonAccess.GetUser().ToString(), ActionPerformed = "Menu Access Modification", ActionDesc = "Menu access modified for user group: " + userGroup, Url = Request.Url.ToString() });
            }
            json.success = true;
            json.result = response.Message;
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}