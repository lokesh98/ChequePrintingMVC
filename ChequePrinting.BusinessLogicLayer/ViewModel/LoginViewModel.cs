using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.ViewModel
{
    public class LoginViewModel
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public string LandingPageUrl { get; set; }
        public bool IsLoggedIn { get; set; }
        public int LoginAttempt { get; set; }
        public int LoginFailedCount { get; set; }
        public bool IsLocked { get; set; }
    }
}
