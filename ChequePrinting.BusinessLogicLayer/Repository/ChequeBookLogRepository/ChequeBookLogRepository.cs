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

namespace ChequePrinting.BusinessLogicLayer.Repository.ChequeBookLogRepository
{
    public class ChequeBookLogRepository : IChequeBookLogRepository
    {

        public bool DownloadToBufferLogs(DataTable dt)
        {
            string sql = DbSql.sql_GetChequeIssueBuffer;

            using (SqlConnection con = ConnectionString.GetConnection())
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    SqlCommandBuilder builder = new SqlCommandBuilder(da);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DataSet ds = new DataSet();
                    DataRow dr;
                    da.FillSchema(ds, SchemaType.Source, "ChqIssueBuffer");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = ds.Tables["ChqIssueBuffer"].NewRow();
                        for (int j = 0; j < ds.Tables["ChqIssueBuffer"].Columns.Count; j++)
                        {
                            dr[j] = dt.Rows[i][j].ToString().Trim();
                        }
                        ds.Tables["ChqIssueBuffer"].Rows.Add(dr);
                    }
                    da.Update(ds, "ChqIssueBuffer");

                }
            }
            ClearBufferData();
            return GetBufferCount() != "0";
        }


        public bool UploadDB2(DataTable dt)
        {
            try
            {
                using (SqlConnection con = ConnectionString.GetConnection())
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        sqlBulkCopy.DestinationTableName = "dbo.DB2data";

                        string sql3 = "TRUNCATE TABLE [DB2data]";
                        DAO.IDU(sql3, CommandType.Text, null);
                        sqlBulkCopy.ColumnMappings.Add("Data", "Data");

                        //con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        //con.Close();
                    }
                }
                return true;
            }
            catch (Exception )
            {
                return false;
            }


        }

        //public bool DownloadToBufferLogs(DataSet ds)
        //{
        //    string sql = DbSql.sql_GetChequeIssueBuffer;
        //    using (SqlConnection con = ConnectionString.GetConnection())
        //    {
        //        using (SqlCommand cmd = con.CreateCommand())
        //        {
        //            cmd.CommandText = sql;
        //            cmd.CommandType = CommandType.Text;
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
        //            DataSet ds1 = new DataSet();
        //            DataRow dr;
        //            da.FillSchema(ds1, SchemaType.Source, "ChqIssueBuffer");

        //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //            {
        //                dr = ds1.Tables["ChqIssueBuffer"].NewRow();
        //                for (int j = 0; j < ds.Tables["ChqIssueBuffer"].Columns.Count; j++)
        //                {
        //                    dr[j] = ds1.Tables[0].Rows[i][j].ToString().Trim();
        //                }
        //                ds1.Tables["ChqIssueBuffer"].Rows.Add(dr);
        //            }
        //            da.Update(ds1, "ChqIssueBuffer");

        //        }
        //    }
        //    ClearBufferData();
        //    return GetBufferCount() != "0";
        //}
        private void ClearBufferData()
        {
            DateTime lastTimeStamp = Helper.GetDate(GetLastTimeStamp());
            string sql1 = "DELETE FROM ChqIssueBuffer WHERE DATEDIFF(ss,'" + lastTimeStamp + "',CAST(CHECKERDATETIME AS DATETIME)) < 1";
            DAO.IDU(sql1, CommandType.Text, null);
        }
        private string GetBufferCount()
        {
            string result = DAO.ExecuteScalar(DbSql.sql_GetChequeBufferCount, CommandType.Text, null);
            return result;
        }
        public IEnumerable<ChequeIssueBufferViewModel> GetBufferLogs(ChequeBookPrintSearchVM searchVM)
        {
            var list = new List<ChequeIssueBufferViewModel>();
            DataSet ds = GetBufferLogsDS(searchVM);
            if (Helper.HasRowsInDataSetTable(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var model = new ChequeIssueBufferViewModel()
                    {
                        CurrencyCode = Helper.GetDBNUllString(row["CURRENCYCODE"]),
                        AccountNo = Helper.GetDBNUllString(row["ACCOUNTNO"]),
                        Branch = new BranchViewModel() { BranchCode = Helper.GetDBNUllString(row["BRANCHCODE"]), BranchName = Helper.GetDBNUllString(row["BRANCHNAME"]) },
                        FullName = Helper.GetDBNUllString(row["FULLNAME"]),
                        RelationShipNo = Helper.GetDBNUllString(row["RELATIONSHIPNO"]),
                        RelationShipType = Helper.GetDBNUllString(row["RELATIONSHIPTYPE"]),
                        ChequeStartNo = Helper.GetInteger(row["CHQSTARTNO"]),
                        IssueDate = Helper.GetDBNUllDateString(row["ISSUEDATE"], Settings.ShortDateFormat),
                        ReqID = Helper.GetDBNUllString(row["REQID"]),
                        NoOfLeaves = Helper.GetInteger(row["NOOFLEAVES"]),
                        BookStatus = Helper.GetDBNUllString(row["BOOKSTATUS"]),
                        MakerID = Helper.GetDBNUllString(row["MAKERID"]),
                        MakerDate = Helper.GetDBNUllString(row["MAKERDATE"]),
                        MakerTime = Helper.GetDBNUllString(row["MAKERTIME"]),
                        MakerDateTime = Helper.GetDBNUllString(row["MAKERDATETIME"]),
                        MakerIPAddress = Helper.GetDBNUllString(row["MAKERIPADDRESS"]),
                        CheckerID = Helper.GetDBNUllString(row["CHECKERID"]),
                        CheckerDate = Helper.GetDBNUllString(row["CHECKERDATE"]),
                        CheckerTime = Helper.GetDBNUllString(row["CHECKERTIME"]),
                        CheckerDateTime = Helper.GetDBNUllString(row["CHECKERDATETIME"]),
                        CheckerIPAddress = Helper.GetDBNUllString(row["CHECKERIPADDRESS"]),
                        StatusFlag = Helper.GetDBNUllString(row["STATUSFLAG"]),
                        NoOfTimesPrinted = Helper.GetInteger(row["NOOFTIMESPRINTED"]),
                        PrintedDate = Helper.GetDBNUllDateString(row["PRINTEDDATE"], Settings.ShortDateFormat),
                        FlatNo = Helper.GetDBNUllString(row["FLATNO"]),
                        BldgName = Helper.GetDBNUllString(row["BLDGNAME"]),
                        NrLandMark = Helper.GetDBNUllString(row["NRLANDMRK"]),
                        Street = Helper.GetDBNUllString(row["STREET"]),
                        TRS = Helper.GetDBNUllString(row["TRS"]),
                        Mobile = Helper.GetDBNUllString(row["MOB"]),
                    };
                    list.Add(model);
                }
            }
            return list;
        }
        public string GetLastTimeStamp()
        {
            DataTable dataTable = DAO.GetTable(DbSql.sql_GetLastTimeStamp, CommandType.Text, null);
            if (dataTable.Rows.Count > 0)
            {
                return dataTable.Rows[0][0].ToString();
            }
            return string.Empty;
        }
        private DataSet GetBufferLogsDS(ChequeBookPrintSearchVM searchVM)
        {
            int startRow = (searchVM.CurrentPage - 1) * searchVM.PerPage + 1;
            int endRow = startRow + searchVM.PerPage - 1;
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Start",startRow),
                new SqlParameter("@Last",endRow)
            };
            return DAO.GetDataSet("sp_GetChqIssueBuffer_New", CommandType.StoredProcedure, param);
        }
        public ResponseViewModel ClearBuffer(string userID)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                   new SqlParameter("@UserID",userID),
                };
                int result = DAO.IDU("p_ClearTranBuffer", CommandType.StoredProcedure, param);
                return new ResponseViewModel() { Message = MsgBox.delete_msg, Status = MsgBox.success_status };
            }
            catch (Exception ex)
            {
                return new ResponseViewModel() { Exception = ex, Message = ex.Message, Status = MsgBox.failed_status };
            }
        }

        public ResponseViewModel PostBufferChequeLogs()
        {
            try
            {
                int result = DAO.IDU("p_PostBufferChqLogs", CommandType.StoredProcedure, null);
                return new ResponseViewModel() { Message = MsgBox.batch_post, Status = MsgBox.success_status };
            }
            catch (Exception ex)
            {
                return new ResponseViewModel() { Exception = ex, Message = ex.Message, Status = MsgBox.failed_status };
            }
        }
    }
}
