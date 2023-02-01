using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.ViewModel
{
    public class UserReportViewModel
    {
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool CanDownload { get; set; }
        public bool CanUndoPrint { get; set; }
        public string LastLogin { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }
}
