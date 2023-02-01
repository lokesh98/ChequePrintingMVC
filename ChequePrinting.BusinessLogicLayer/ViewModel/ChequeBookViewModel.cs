using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.ViewModel
{
    public class ChequeBookViewModel
    {
        public string CustomerName { get; set; }
        public string AccountNo { get; set; }
        public string OnlineBanking { get; set; }
        public string AccountNo1 { get; set; }
        public string BankCode { get; set; }
        public BranchViewModel Branch { get; set; }
        public string RelationShipType { get; set; }
        public string NoOfLeaves { get; set; }
        public string CheckerID { get; set; }
        public int IssueID { get; set; }
        public string FlatNo { get; set; }
        public string BldgName { get; set; }
        public string NRLandMark { get; set; }
        public string Street { get; set; }
        public string TRS { get; set; }
        public string Mobile { get; set; }
        public string ChequeStartNo { get; set; }
        public string ChequeNo1 { get; set; }
        public string ChequeNo2 { get; set; }
        public string ChequeNo3 { get; set; }

        public string UpdatedBy { get; set; }
    }
}
