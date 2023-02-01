using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.DataAccessLayer
{
    public static class Db2DAO
    {
        public static DataSet GetDataSet(string sql, CommandType cmdType, OdbcParameter[] param)
        {
            using (OdbcConnection con = ConnectionString.GetDB2Connection())
            {
                DataSet ds = new DataSet();
                using (OdbcCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = cmdType;
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    using (OdbcDataAdapter da = new OdbcDataAdapter(cmd))
                    {
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
        }
    }
}
