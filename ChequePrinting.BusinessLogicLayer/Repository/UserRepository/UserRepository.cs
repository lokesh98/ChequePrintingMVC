using ChequePrinting.BusinessLogicLayer.SqlQuery;
using ChequePrinting.BusinessLogicLayer.ViewModel;
using ChequePrinting.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ChequePrinting.BusinessLogicLayer.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        public IEnumerable<UserViewModel> GetUserList(UserSearchVM searchVM)
        {
            var list = new List<UserViewModel>();
            DataSet ds = GetUsersDS(searchVM);
            if (Helper.HasRowsInDataSetTable(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var model = new UserViewModel()
                    {
                        UserCode = Helper.GetDBNUllString(row["USER_ID"]),
                        UserName = Helper.GetDBNUllString(row["USER_NAME"]),
                        Email = Helper.GetDBNUllString(row["EMAIL"]),
                        RoleId = Helper.GetInteger(row["RoleId"]),
                        RoleName = Helper.GetDBNUllString(row["RoleDesc"]),
                        StatusId = Helper.GetInteger(row["STATUS_ID"]),
                        StatusName = Helper.GetDBNUllString(row["STATUS_NAME"]),
                        IsLogin = Helper.GetInteger(row["ISLOGIN"]),
                        CanDownload = Helper.GetDBNUllBoolean(row["CAN_DOWNLOAD"].ToString()),
                        IsLocked = Helper.GetDBNUllBoolean(row["IsLocked"].ToString()),
                        CanUndoPrint = Helper.GetDBNUllBoolean(row["CAN_UNDO_PRINT"].ToString()),
                        LastLogin = Helper.GetDBNUllDateString(row["LastLogin"], Settings.LongDateFormat),
                        CreatedBy = Helper.GetDBNUllString(row["CREATED_BY"]),
                        ModifiedBy = Helper.GetDBNUllString(row["MODIFIED_BY"]),
                        CreatedDate = Helper.GetDBNUllDateString(row["CREATED_DATE"], Settings.LongDateFormat),
                        ModifiedDate = Helper.GetDBNUllDateString(row["MODIFIED_DATE"], Settings.LongDateFormat)
                    };
                    list.Add(model);
                }
            }
            searchVM.Total = Helper.GetInteger(ds.Tables[1].Rows[0]["TotalRow"]);
            return list;
        }
        private DataSet GetUsersDS(UserSearchVM searchVM)
        {
            int startRow = (searchVM.CurrentPage - 1) * searchVM.PerPage + 1;
            int endRow = startRow + searchVM.PerPage - 1;
            if (searchVM.UserName != null)
            {
                searchVM.UserName = searchVM.UserName.Trim();
            }
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@SearchText",searchVM.UserName),
                new SqlParameter("@Start",startRow),
                new SqlParameter("@Last",endRow)
            };
            DataSet ds = DAO.GetDataSet("SP_GETUSERLIST", CommandType.StoredProcedure, param);
            return ds;
        }
        public ResponseViewModel AddNewUser(UserViewModel model)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                      new SqlParameter("@USER_CODE", model.UserCode),
                      new SqlParameter("@USER_NAME", model.UserName),
                      new SqlParameter("@EMAIL", model.Email),
                      new SqlParameter("@ROLE_ID", model.RoleId),
                      new SqlParameter("@STATUS_ID", model.StatusId),
                      new SqlParameter("@IS_LOGIN", model.IsLogin),
                      new SqlParameter("@CanDownload", model.CanDownload),
                      new SqlParameter("@CanUndoPrint", model.CanUndoPrint),
                      new SqlParameter("@IsLocked", model.IsLocked),
                      new SqlParameter("@CREATED_BY", string.IsNullOrEmpty(model.CreatedBy) ? (object)DBNull.Value : model.CreatedBy),
                };
                int result = DAO.IDU("SP_INSERT_USER", CommandType.StoredProcedure, param);
                return new ResponseViewModel() { Message = MsgBox.save_msg, Status = MsgBox.success_status };
            }
            catch (Exception ex)
            {
                return new ResponseViewModel() { Exception = ex, Message = ex.Message, Status = MsgBox.failed_status };
            }
        }
        public ResponseViewModel UpdateUser(UserViewModel model)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                      new SqlParameter("@USER_CODE", model.UserCode),
                      new SqlParameter("@USER_NAME", model.UserName),
                      new SqlParameter("@EMAIL", model.Email),
                      new SqlParameter("@ROLE_ID", model.RoleId),
                      new SqlParameter("@STATUS_ID", model.StatusId),
                      new SqlParameter("@IS_LOGIN", model.IsLogin),
                      new SqlParameter("@CanDownload", model.CanDownload),
                      new SqlParameter("@CanUndoPrint", model.CanUndoPrint),
                       new SqlParameter("@IsLocked", model.IsLocked),
                      new SqlParameter("@ModifiedBy", string.IsNullOrEmpty(model.ModifiedBy) ? (object)DBNull.Value : model.ModifiedBy),
                };
                int result = DAO.IDU("SP_UPDATE_USER", CommandType.StoredProcedure, param);
                return new ResponseViewModel() { Message = MsgBox.update_msg, Status = MsgBox.success_status, LastId = result };
            }
            catch (Exception ex)
            {
                return new ResponseViewModel() { Exception = ex, Message = ex.Message, Status = MsgBox.failed_status };
            }
        }
        public ResponseViewModel DeleteUser(string userId)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                   new SqlParameter("@UserID",userId),
                };
                int result = DAO.IDU(DbSql.sql_DeleteUserByID, CommandType.Text, param);
                return new ResponseViewModel() { Message = MsgBox.delete_msg, Status = MsgBox.success_status };
            }
            catch (Exception ex)
            {
                return new ResponseViewModel() { Exception = ex, Message = ex.Message, Status = MsgBox.failed_status };
            }
        }
        public UserViewModel GetUserByUserID(string userId)
        {
            var model = new UserViewModel();
            DataSet ds = GetUserByIDDS(userId);
            if (Helper.HasRowsInDataSetTable(ds))
            {
                var row = ds.Tables[0].Rows[0];
                model.UserCode = Helper.GetDBNUllString(row["USER_ID"]);
                model.UserName = Helper.GetDBNUllString(row["USER_NAME"]);
                model.Email = Helper.GetDBNUllString(row["EMAIL"]);
                model.RoleId = Helper.GetInteger(row["RoleId"]);
                model.RoleName = Helper.GetDBNUllString(row["RoleDesc"]);
                model.StatusId = Helper.GetInteger(row["STATUS_ID"]);
                model.StatusName = Helper.GetDBNUllString(row["STATUS_NAME"]);
                model.IsLogin = Helper.GetInteger(row["ISLOGIN"]);
                model.IsLocked = Helper.GetDBNUllBoolean(row["ISLOCKED"].ToString());
                model.CanDownload = Helper.GetDBNUllBoolean(row["CAN_DOWNLOAD"].ToString());
                model.CanUndoPrint = Helper.GetDBNUllBoolean(row["CAN_UNDO_PRINT"].ToString());
            }

            return model;
        }

        private DataSet GetUserByIDDS(string userId)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@UserID",string.IsNullOrEmpty(userId) ? (object)DBNull.Value : userId),
            };
            DataSet ds = DAO.GetDataSet("sp_GetUserByUserID", CommandType.StoredProcedure, param);
            return ds;
        }
    }
}
