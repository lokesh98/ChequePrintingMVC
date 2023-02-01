using ChequePrinting.BusinessLogicLayer.SqlQuery;
using ChequePrinting.BusinessLogicLayer.ViewModel;
using ChequePrinting.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.Repository.DailyChequeBookPrintRptRepository
{
    public class DailyChequeBookPrintRptRepository : IDailyChequeBookPrintRptRepository
    {
        public ResponseViewModel DestroyAccount(DailyChequeBookRptSearchVM searchVM)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                   new SqlParameter("@AccountNo",searchVM.AccountNo),
                   new SqlParameter("@StartDate",searchVM.FromDate),
                   new SqlParameter("@EndDate",searchVM.ToDate),
                };
                int result = DAO.IDU(DbSql.sql_DestroyAccount, CommandType.Text, param);
                return new ResponseViewModel() { Message = MsgBox.update_msg, Status = MsgBox.success_status };
            }
            catch (Exception ex)
            {
                return new ResponseViewModel() { Exception = ex, Message = ex.Message, Status = MsgBox.failed_status };
            }
        }

        public IEnumerable<DailyChequeBookReportViewModel> SendSMS(DailyChequeBookRptSearchVM searchVM)
        {
            var list = new List<DailyChequeBookReportViewModel>();
            DataSet ds = GetDailyChequePrintReportDS(searchVM);
            if (Helper.HasRowsInDataSetTable(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var model = new DailyChequeBookReportViewModel()
                    {
                        ChequeStartNo = Helper.GetInteger(row["CHQSTARTNO"]),
                        CurrencyCode = Helper.GetDBNUllString(row["CURRENCYCODE"]),
                        IssueID = Helper.GetInteger(row["ISSUEID"]),
                        AccountNo = Helper.GetDBNUllString(row["ACCOUNTNO"]),
                        LStatus = Helper.GetDBNUllString(row["L_STATUS"]),
                        BranchCode = Helper.GetDBNUllString(row["BRANCHCODE"]),
                        FullName = Helper.GetDBNUllString(row["FULLNAME"]),
                        RelationShipType = Helper.GetDBNUllString(row["RELATIONSHIPTYPE"]),
                        NoOfLeaves = Helper.GetInteger(row["NOOFLEAVES"]),
                        PrintDate = Helper.GetDBNUllDateString(row["NOOFLEAVES"], Settings.LongDateFormat),
                        InfoCode = Helper.GetDBNUllString(row["INFOCODE"]),
                        Mob = Helper.GetDBNUllString(row["MOB"]),
                        ReviewedBy = Helper.GetDBNUllString(row["L_REVIEWEDBY"]),
                        ReviewedDate = Helper.GetDBNUllDateString(row["L_REVIEWEDDATE"], Settings.LongDateFormat),
                        MakerName = Helper.GetDBNUllString(row["MakerName"]),
                        ReviewerName = Helper.GetDBNUllString(row["ReviewerName"]),
                    };

                        SqlParameter[] param = new SqlParameter[]
                        {
                   new SqlParameter("@MOB", model.Mob),
                new SqlParameter("@MAKERNAME",model.MakerName),
                new SqlParameter("@L_PRINTEDDATE",model.PrintDate),
                new SqlParameter("@L_REVIEWEDBY",model.ReviewedBy),
                new SqlParameter("@L_REVIEWEDDATE",model.ReviewedDate),
                 new SqlParameter("@ACCOUNTNO",model.AccountNo)
                        };
                        int result = DAO.IDU(DbSql.sql_SendSMS, CommandType.Text, param);
                   


                    list.Add(model);
                    
                }
            }
            searchVM.Total = Helper.GetInteger(ds.Tables[1].Rows[0]["TotalRow"]);
            //return new DailyChequeBookReportViewModel() {  "SMS sent Successfully ",  "Sucess" };

            return list;
        }

        public IEnumerable<DailyChequeBookReportViewModel> GetDailyChequePrintReport(DailyChequeBookRptSearchVM searchVM)
        {
            var list = new List<DailyChequeBookReportViewModel>();
            DataSet ds = GetDailyChequePrintReportDS(searchVM);
            if (Helper.HasRowsInDataSetTable(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var model = new DailyChequeBookReportViewModel()
                    {
                        ChequeStartNo = Helper.GetInteger(row["CHQSTARTNO"]),
                        CurrencyCode = Helper.GetDBNUllString(row["CURRENCYCODE"]),
                        IssueID = Helper.GetInteger(row["ISSUEID"]),
                        AccountNo = Helper.GetDBNUllString(row["ACCOUNTNO"]),
                        LStatus = Helper.GetDBNUllString(row["L_STATUS"]),
                        BranchCode = Helper.GetDBNUllString(row["BRANCHCODE"]),
                        FullName = Helper.GetDBNUllString(row["FULLNAME"]),
                        RelationShipType = Helper.GetDBNUllString(row["RELATIONSHIPTYPE"]),
                        NoOfLeaves = Helper.GetInteger(row["NOOFLEAVES"]),
                        PrintDate = Helper.GetDBNUllDateString(row["NOOFLEAVES"], Settings.LongDateFormat),
                        InfoCode = Helper.GetDBNUllString(row["INFOCODE"]),
                        Mob = Helper.GetDBNUllString(row["MOB"]),
                        ReviewedBy = Helper.GetDBNUllString(row["L_REVIEWEDBY"]),
                        ReviewedDate = Helper.GetDBNUllDateString(row["L_REVIEWEDDATE"], Settings.LongDateFormat),
                        MakerName = Helper.GetDBNUllString(row["MakerName"]),
                        ReviewerName = Helper.GetDBNUllString(row["ReviewerName"]),
                    };
                    list.Add(model);
                }
            }
            searchVM.Total = Helper.GetInteger(ds.Tables[1].Rows[0]["TotalRow"]);
            return list;
        }

        private DataSet GetDailyChequePrintReportDS(DailyChequeBookRptSearchVM searchVM)
        {
            int startRow = (searchVM.CurrentPage - 1) * searchVM.PerPage + 1;
            int endRow = startRow + searchVM.PerPage - 1;
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@BranchCode", string.IsNullOrEmpty(searchVM.BranchCode) ? (object)DBNull.Value : searchVM.BranchCode),
                new SqlParameter("@FromDate",searchVM.FromDate),
                new SqlParameter("@ToDate",searchVM.ToDate),
                new SqlParameter("@Start",startRow),
                new SqlParameter("@Last",endRow)
            };
            DataSet ds = DAO.GetDataSet("sp_GetPeriodicPrintReport_New", CommandType.StoredProcedure, param);
            return ds;
        }
    }
}
