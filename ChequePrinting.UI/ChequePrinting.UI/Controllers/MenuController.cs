using ChequePrinting.BusinessLogicLayer.Repository.MenuRepository;
using ChequePrinting.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChequePrinting.UI.Controllers
{
    public class MenuController : BaseController
    {
        private readonly IMenuRepository menuRepository;
        public MenuController(IMenuRepository _menuRepository)
        {
            this.menuRepository = _menuRepository;
        }
        public virtual PartialViewResult GetMenuList()
        {
            var menuList = menuRepository.GetMenuList(JsonAccess.UserRole());
            return PartialView("_MenuBar", menuList);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}