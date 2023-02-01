using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.ViewModel
{
    public class ChequeIssueMasterViewModel: ChequeIssueBufferViewModel
    {
        public long IssueID { get; set; }
        public string OnlineBanking { get; set; }
        public string LStatus { get; set; }
        public string LPrintedBy { get; set; }
        public string LPrintedDate { get; set; }
        public int LNoOfPrint { get; set; }
        public string LReviewedBy { get; set; }
        public string LReviewedDate { get; set; }
        public string CStatus { get; set; }
        public string CPrintedBy { get; set; }
        public string CPrintedDate { get; set; }
        public int CNoOfPrint { get; set; }
        public string InfoCode { get; set; }
    }
    public class ChequeBookPrintSearchVM
    {
        public string UserID { get; set; }
        public string ChequeType { get; set; }
        public bool FilterByMakerID { get; set; }
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
    }
}
