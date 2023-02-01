using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.ViewModel
{
    public class UserViewModel
    {
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int StatusId { get; set; }        
        public string StatusName { get; set; }
        public string Email { get; set; }
        public int IsLogin { get; set; }
        public bool IsLocked { get; set; }
        public bool CanDownload { get; set; }
        public bool CanUndoPrint { get; set; }
        public string LastLogin { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }
    public class UserSearchVM
    {
        public string UserName { get; set; }
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
    }
}
