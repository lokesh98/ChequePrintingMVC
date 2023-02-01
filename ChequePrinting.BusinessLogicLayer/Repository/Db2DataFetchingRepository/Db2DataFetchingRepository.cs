using ChequePrinting.BusinessLogicLayer.SqlQuery;
using ChequePrinting.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.Repository.Db2DataFetchingRepository
{
    public class Db2DataFetchingRepository : IDb2DataFetchingRepository
    {
        public DataSet GetDataFromDB2(DateTime lastTimeStamp)
        {
            //string customDate = lastTimeStamp.ToString("MM/dd/yyyy") + "','" + lastTimeStamp.ToString("hh:mm:ss");
            string customDate = lastTimeStamp.ToString("yyyy-MM-dd") + " " + lastTimeStamp.ToString("hh:mm:ss");
            //string sql = "Insert into IssueLog ChqIssueBuffer WHERE DATEDIFF(ss,'" + customDate + "',CAST(CHECKERDATETIME AS DATETIME)) < 1";
            //DAO.IDU(sql1, CommandType.Text, null);
            OdbcParameter[] parameters = new OdbcParameter[]
             {

                  //new OdbcParameter("@LastTimeStamp","2021-11-11 16:24:00")
                  new OdbcParameter("@LastTimeStamp",customDate)

             };
            DataSet ds = Db2DAO.GetDataSet(DB2Sql.sql_GetChequeBookLogs, CommandType.Text, parameters);
            return ds;
        }
    }
}
