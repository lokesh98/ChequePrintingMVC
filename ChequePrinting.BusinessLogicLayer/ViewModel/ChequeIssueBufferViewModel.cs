using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.ViewModel
{
    public class ChequeIssueBufferViewModel
    {
        public string CurrencyCode { get; set; }
        public string AccountNo { get; set; }
        public BranchViewModel Branch { get; set; }
        public string FullName { get; set; }
        public string RelationShipNo { get; set; }
        public string RelationShipType { get; set; }
        public long ChequeStartNo { get; set; }
        public string IssueDate { get; set; }
        public string ReqID { get; set; }
        public int NoOfLeaves { get; set; }
        public string BookStatus { get; set; }
        public string MakerID { get; set; }
        public string MakerDate { get; set; }
        public string MakerTime { get; set; }
        public string MakerDateTime { get; set; }
        public string MakerIPAddress { get; set; }
        public string CheckerID { get; set; }
        public string CheckerDate { get; set; }
        public string CheckerTime { get; set; }
        public string CheckerDateTime { get; set; }
        public string CheckerIPAddress { get; set; }
        public string StatusFlag { get; set; }
        public int NoOfTimesPrinted { get; set; }
        public string PrintedDate { get; set; }
        public string FlatNo { get; set; }
        public string BldgName { get; set; }
        public string NrLandMark { get; set; }
        public string Street { get; set; }
        public string TRS { get; set; }
        public string Mobile { get; set; }
    }
}
