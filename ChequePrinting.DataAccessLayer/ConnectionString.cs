using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.DataAccessLayer
{
    public static class ConnectionString
    {
        public static SqlConnection GetConnection()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(CQPrintingConnectionString);
            builder.Password = Decrypt(builder.Password);

            SqlConnection con = new SqlConnection(builder.ToString());
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            return con;

        }
        public static string CQPrintingConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString;
            }
        }

        private static string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public static OdbcConnection GetDB2Connection()
        {
            try
            {
                OdbcConnection con = new OdbcConnection(DB2ConnectionString);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                return con;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string DB2ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["DB2Connection"].ConnectionString;
            }
        }
    }
}
