using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.ViewModel
{
    public class DailyChequeBookReportViewModel
    {
        public int ChequeStartNo { get; set; }
        public int IssueID { get; set; }
        public string CurrencyCode { get; set; }
        public string AccountNo { get; set; }
        public string LStatus { get; set; }
        public string BranchCode { get; set; }
        public string FullName { get; set; }
        public string RelationShipType { get; set; }
        public int NoOfLeaves { get; set; }
        public string PrintDate { get; set; }
        public string InfoCode { get; set; }
        public string Mob { get; set; }
        public string ReviewedBy { get; set; }
        public string ReviewedDate { get; set; }
        public string MakerName { get; set; }
        public string ReviewerName { get; set; }
        //public string Status { get; set; }
        //public string Message { get; set; }
        //public Exception Exception { get; set; }

    }

    public class DailyChequeBookRptSearchVM
    {
        public string BranchCode { get; set; }
        public string AccountNo { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
    }
}
