using ChequePrinting.BusinessLogicLayer.ViewModel;
using ChequePrinting.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.Repository.RequisitionRepository
{
    public class RequisitionRepository : IRequisitionFormRepository
    {
        public ResponseViewModel UpdateRequisitionFormPrintFlag(string issueID, string userID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                      new SqlParameter("@ISSUEID", issueID),
                      new SqlParameter("@UserID", userID),
                };
                int result = DAO.IDU("p_UpdateRequisitionFormPrintFlag", CommandType.StoredProcedure, param);
                return new ResponseViewModel() { Message = MsgBox.update_msg, Status = MsgBox.success_status, LastId = result };
            }
            catch (Exception ex)
            {
                return new ResponseViewModel() { Exception = ex, Message = ex.Message, Status = MsgBox.failed_status };
            }
        }

        public IEnumerable<ChequeBookViewModel> GetReq(string issueID)
        {
            var list = new List<ChequeBookViewModel>();
            DataSet ds = GetReqDS(issueID);
            if (Helper.HasRowsInDataSetTable(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var model = new ChequeBookViewModel()
                    {
                        IssueID = Helper.GetInteger(row["ISSUEID"]),
                        AccountNo = Helper.GetDBNUllString(row["ACCOUNTNO"]),
                        OnlineBanking = Helper.GetDBNUllString(row["ONLINEBANKING"]),
                        Branch = new BranchViewModel() { BranchCode = Helper.GetDBNUllString(row["BRANCHCODE"]), BranchName = Helper.GetDBNUllString(row["BRANCHNAME"]) },
                        AccountNo1 = Helper.GetDBNUllString(row["ACCOUNTNO1"]),
                        CustomerName = Helper.GetDBNUllString(row["CUSTOMERNAME"]),
                        BankCode = Helper.GetDBNUllString(row["BANKCODE"]),
                        RelationShipType = Helper.GetDBNUllString(row["RELATIONSHIPTYPE"]),
                        NoOfLeaves = Helper.GetDBNUllString(row["NOOFLEAVES"]),
                        CheckerID = Helper.GetDBNUllString(row["CHECKERID"]),
                        FlatNo = Helper.GetDBNUllString(row["FLATNO"]),
                        BldgName = Helper.GetDBNUllString(row["BLDGNAME"]),
                        NRLandMark = Helper.GetDBNUllString(row["NRLANDMRK"]),
                        Street = Helper.GetDBNUllString(row["STREET"]),
                        TRS = Helper.GetDBNUllString(row["TRS"]),
                        Mobile = Helper.GetDBNUllString(row["MOB"]),
                        ChequeStartNo = Helper.GetDBNUllString(row["CHQSTARTNO"]),
                        ChequeNo1 = Helper.GetDBNUllString(row["ChqNo_1"]),
                        ChequeNo2 = Helper.GetDBNUllString(row["ChqNo_2"]),
                        ChequeNo3 = Helper.GetDBNUllString(row["ChqNo_3"]),

                    };
                    list.Add(model);

                }
            }
            return list;
        }

        private DataSet GetReqDS(string issueID)
        {
            SqlParameter[] param = new SqlParameter[]
             {
                new SqlParameter("@ChqBookID",issueID)
             };
            DataSet ds = DAO.GetDataSet("p_GetReqItemToPrint", CommandType.StoredProcedure, param);
            return ds;
        }

    }


}
