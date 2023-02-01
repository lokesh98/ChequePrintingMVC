using ChequePrinting.BusinessLogicLayer.ViewModel;
using ChequePrinting.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.Repository.AuditLogRepository
{
    public class AuditLogRepository : IAuditLogRepository
    {
        public ResponseViewModel AddAuditLog(AuditLogViewModel model)
        {
            try
            {
                var customUrl = model.Url.Split('/');
                string ipAddr = Helper.GetCurrentUserIPAddr();
                SqlParameter[] param = new SqlParameter[]
                {
                     new SqlParameter("@IP",ipAddr),
                     new SqlParameter("@MachineName",Helper.DeviceName(ipAddr)),
                     new SqlParameter("@UserLoginID",model.UserId),
                     new SqlParameter("@Event", string.IsNullOrEmpty(model.ActionPerformed) ? (object)DBNull.Value : model.ActionPerformed),
                     new SqlParameter("@EventDesc", string.IsNullOrEmpty(model.ActionDesc) ? (object)DBNull.Value : model.ActionDesc),
                     new SqlParameter("@OldValue", string.IsNullOrEmpty(model.OldValue) ? (object)DBNull.Value : model.OldValue),
                     new SqlParameter("@NewValue", string.IsNullOrEmpty(model.NewValue) ? (object)DBNull.Value : model.NewValue),
                     new SqlParameter("@ReflectedUserID", string.IsNullOrEmpty(model.ReflectedUserID) ? (object)DBNull.Value : model.ReflectedUserID),
                     new SqlParameter("@Url",customUrl[3]+"/"+customUrl[4]),
                };
                int result = DAO.IDU("sp_InsertUserActivityLog_New", CommandType.StoredProcedure, param);
                return new ResponseViewModel() { Message = MsgBox.save_msg, Status = MsgBox.success_status };
            }
            catch (Exception ex)
            {
                return new ResponseViewModel() { Exception = ex, Message = ex.Message, Status = MsgBox.failed_status };
            }
        }

        public IEnumerable<AuditLogViewModel> GetAuditLogs(AuditSearchVM searchVM)
        {
            var list = new List<AuditLogViewModel>();
            DataSet ds = GetAuditLogList(searchVM);
            if (Helper.HasRowsInDataSetTable(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var model = new AuditLogViewModel()
                    {
                        Id = Helper.GetInteger(row["Id"]),
                        IPAddress = Helper.GetDBNUllString(row["IP"]),
                        MachineName = Helper.GetDBNUllString(row["MachineName"]),
                        ActionPerformed = Helper.GetDBNUllString(row["Event"]),
                        ActionDesc = Helper.GetDBNUllString(row["EventDesc"]),
                        OldValue = Helper.GetDBNUllString(row["OldValue"]),
                        NewValue = Helper.GetDBNUllString(row["NewValue"]),
                        TimeStamp = Helper.GetDBNUllDateString(row["TimeStamp"], Settings.ShortDateFormat),
                        Url = Helper.GetDBNUllString(row["Url"]),
                        UserId = Helper.GetDBNUllString(row["UserInfo"]),
                        ReflectedUserID = Helper.GetDBNUllString(row["ReflectedUserID"]),
                        TimeSpan = Helper.GetDBNUllString(row["TimeSpan"])
                    };
                    list.Add(model);
                }
            }
            searchVM.Total = Helper.GetInteger(ds.Tables[1].Rows[0]["TotalRow"]);
            return list;
        }

        private DataSet GetAuditLogList(AuditSearchVM searchVM)
        {
            int startRow = (searchVM.CurrentPage - 1) * searchVM.PerPage + 1;
            int endRow = startRow + searchVM.PerPage - 1;
            if (searchVM.UserId != null)
            {
                searchVM.UserId = searchVM.UserId.Trim();
            }
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@FromDate", string.IsNullOrEmpty(searchVM.FromDate) ? (object)DBNull.Value : searchVM.FromDate),
                new SqlParameter("@ToDate", string.IsNullOrEmpty(searchVM.ToDate) ? (object)DBNull.Value : searchVM.ToDate),
                new SqlParameter("@UserLoginId", string.IsNullOrEmpty(searchVM.UserId) ? (object)DBNull.Value : searchVM.UserId),
                new SqlParameter("@Start",startRow),
                new SqlParameter("@Last",endRow)
            };
            DataSet ds = DAO.GetDataSet("sp_GetAuditLogReport", CommandType.StoredProcedure, param);
            return ds;
        }
    }
}
