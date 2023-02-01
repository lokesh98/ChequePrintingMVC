using ChequePrinting.BusinessLogicLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.Repository.MenuRepository
{
    public interface IMenuRepository
    {
        IList<MenuViewModel> GetMenuList(string roleDesc);
        IEnumerable<MenuAccessViewModel> GetMenuMapping(string userGroups);
        ResponseViewModel SaveRoleMenuMapping(MenuAccessViewModel model);
        ResponseViewModel DeleteMenuAccess(string userGroup);
        IList<MenuViewModel> GetMenuForAccessMgmt();
    }
}
