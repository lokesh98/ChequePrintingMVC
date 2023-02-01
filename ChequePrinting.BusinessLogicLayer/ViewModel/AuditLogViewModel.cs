using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.ViewModel
{
    public class AuditLogViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ActionPerformed { get; set; } //Request type
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string ActionDesc { get; set; } //AuditDesc
        public string IPAddress { get; set; }
        public string MachineName { get; set; }  
        public string Url { get; set; }
        public string TimeStamp { get; set; }
        public string TimeSpan { get; set; }
        public string ReflectedUserID { get; set; }
    }
    public class AuditSearchVM
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string UserId { get; set; }
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
    }
}
