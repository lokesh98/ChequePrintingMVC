using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.ViewModel
{
    public class UserRolesViewModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class UserStatusViewModel
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }

    public class IslockedViewModel
    {
        public string IsLockedId { get; set; }
        public string IslockedName { get; set; }
    }


}
