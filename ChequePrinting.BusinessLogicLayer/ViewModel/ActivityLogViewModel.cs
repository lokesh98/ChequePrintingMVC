using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.ViewModel
{
    public class ActivityLogViewModel
    {
        public int Id { get; set; }
        public string IP { get; set; }
        public string MachineName { get; set; }
        public string UserInfo { get; set; }
        public string Event { get; set; }
        public string User_Login_Id { get; set; }
        public string Request_Type { get; set; }
        public string Old_Value { get; set; }
        public string New_Value { get; set; }
        public string Url { get; set; }
        public string TimeStamp { get; set; }
    }

    public class LogSearchVM
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
    }
   
}
