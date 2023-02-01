using ChequePrinting.BusinessLogicLayer.SqlQuery;
using ChequePrinting.DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ChequePrinting.BusinessLogicLayer.Repository.LoggerRepository
{
    public class Logger : ILogger
    {
        public void LogError(Exception exception)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@ExceptionSource",exception.StackTrace.ToString()),
                new SqlParameter("@ExceptionMsg", exception.Message.ToString()),
                new SqlParameter("@ExceptionType",exception.GetType().Name.ToString()),
                new SqlParameter("@ExceptionURL",HttpContext.Current.Request.Url.ToString())
            };
            DAO.IDU(DbSql.sp_LogError, CommandType.Text, param);
        }
    }
}
