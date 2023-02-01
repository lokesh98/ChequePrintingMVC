using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.DataAccessLayer
{
    public static class DAO
    {
        //Static Method for performing CRUD operations
        public static int IDU(string sql, CommandType cmdType, SqlParameter[] param)
        {
            using (SqlConnection con = ConnectionString.GetConnection())
            {
                using (SqlTransaction trans = con.BeginTransaction())
                {
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        cmd.CommandType = cmdType;
                        cmd.CommandTimeout = 0;
                        cmd.Transaction = trans;
                        if (param != null)
                        {
                            cmd.Parameters.AddRange(param);
                        }
                        try
                        {
                            int i = cmd.ExecuteNonQuery();
                            trans.Commit();
                            return i;
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            throw ex;
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            }
        }

        public static DataTable GetTable(string sql, CommandType cmdType, SqlParameter[] param)
        {
            using (SqlConnection con = ConnectionString.GetConnection())
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = cmdType;
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }
        public static DataSet GetDataSet(string sql, CommandType cmdType, SqlParameter[] param)
        {
            using (SqlConnection con = ConnectionString.GetConnection())
            {
                DataSet ds = new DataSet();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = cmdType;
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
        }

        public static string ExecuteScalar(string sql, CommandType cmdType, SqlParameter[] param)
        {
            string res = "";
            using (SqlConnection con = ConnectionString.GetConnection())
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = cmdType;
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    res = Convert.ToString(cmd.ExecuteScalar());
                }
            }
            return res;
        }
        public static List<Object> Getlist(string sql, CommandType cmdType, SqlParameter[] param)
        {
            List<Object> drlist = new List<Object>();
            using (SqlConnection con = ConnectionString.GetConnection())
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = cmdType;
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        foreach (DataRow row in dt.Rows)
                        {
                            drlist.Add((DataRow)row);
                        }
                    }

                    return drlist;
                }
            }
        }
        public static OutputMessage ExecuteProcedure(string StoredProcName, SqlParameter[] param)
        {
            OutputMessage outputMessage = new OutputMessage();
            try
            {
                using (SqlConnection con = ConnectionString.GetConnection())
                {
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = StoredProcName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        if (param != null)
                        {
                            cmd.Parameters.AddRange(param);
                        }

                        cmd.Parameters["@v_out_status"].Direction = ParameterDirection.Output;
                        cmd.Parameters["@v_out_message"].Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        outputMessage.Status = cmd.Parameters["@v_out_status"].Value.ToString();
                        outputMessage.Message = cmd.Parameters["@v_out_message"].Value.ToString();

                        return outputMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                outputMessage.Message = ex.Message;
                outputMessage.Exception = ex;
                outputMessage.Status = "failed";
                return outputMessage;
            }
        }

        public static int IDU(object sql_InserBuffer, CommandType storedProcedure, SqlParameter[] param)
        {
            throw new NotImplementedException();
        }
    }
}
