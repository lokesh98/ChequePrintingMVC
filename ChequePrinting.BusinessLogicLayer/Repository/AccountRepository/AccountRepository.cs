using ChequePrinting.BusinessLogicLayer.ViewModel;
using ChequePrinting.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.Repository.AccountRepository
{
    public class AccountRepository : IAccountRepository
    {
        public LoginViewModel GetUserByUserID(string userId)
        {
            DataSet ds = GetUserProfile(userId);
            LoginViewModel model = new LoginViewModel();
            if (Helper.HasRowsInDataSetTable(ds))
            {
                var row = ds.Tables[0].Rows[0];
                model.UserID = Helper.GetDBNUllString(row["USER_ID"]);
                model.LandingPageUrl = Helper.GetDBNUllString(row["LandingPageURL"]);
                model.UserName = Helper.GetDBNUllString(row["USER_NAME"]);
                model.RoleID = Helper.GetDBNUllString(row["RoleId"]);
                model.RoleName = Helper.GetDBNUllString(row["RoleDesc"]);
                model.IsLoggedIn = Convert.ToBoolean(row["IsLogin"]);
                model.LoginAttempt = Helper.GetInteger(row["LoginAttempt"]);
                model.LoginFailedCount = Helper.GetInteger(row["LoginFailedCount"]);
                model.IsLocked = Helper.GetDBNUllBoolean(row["IsLockedOut"].ToString());
            }
            return model;
        }

        public ResponseViewModel UpdateLoginInfo(string userID, string flag, int failedAttept)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]
                {
                     new SqlParameter("@Flag",flag),
                     new SqlParameter("@UserId",userID),
                     new SqlParameter("@LoginFailedCount",failedAttept),
                };
                var result = DAO.IDU("sp_UpdateUserLoginStatus", CommandType.StoredProcedure, param);
                return new ResponseViewModel() { Status = MsgBox.success_status };
            }
            catch (Exception ex)
            {
                return new ResponseViewModel() { Exception = ex, Message = ex.Message, Status = MsgBox.failed_status };
            }
        }

        public bool ValidateUser(string userId)
        {
            SqlParameter[] param = new SqlParameter[]
            {
              new SqlParameter("@UserId",userId),
            };
            var result = DAO.ExecuteScalar("sp_CheckUserExists", CommandType.StoredProcedure, param);
            if (result == "1")
            {
                return true;
            }
            return false;
        }
        private DataSet GetUserProfile(string userId)
        {
            SqlParameter[] param = new SqlParameter[]
             {
               new SqlParameter("@UserID",userId)
             };
            DataSet ds = DAO.GetDataSet("sp_GetUserLoginProfile_New", CommandType.StoredProcedure, param);
            return ds;
        }
    }
}
