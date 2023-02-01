using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ChequePrinting.BusinessLogicLayer
{
    public static class Helper
    {
        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
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
        public static DataTable ConvertToDataTable(IEnumerable source)
        {
            var table = new DataTable();
            int index = 0;
            var properties = new List<PropertyInfo>();
            foreach (var obj in source)
            {
                if (index == 0)
                {
                    foreach (var property in obj.GetType().GetProperties())
                    {
                        if (Nullable.GetUnderlyingType(property.PropertyType) != null)
                        {
                            continue;
                        }
                        properties.Add(property);
                        table.Columns.Add(new DataColumn(property.Name, property.PropertyType));
                    }
                }
                object[] values = new object[properties.Count];
                for (int i = 0; i < properties.Count; i++)
                {
                    values[i] = properties[i].GetValue(obj);
                }
                table.Rows.Add(values);
                index++;
            }
            return table;
        }
        public static List<T> ConvertToList<T>(DataTable dt)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            var columnNames = dt.Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName)
                .ToList();
            var objectProperties = typeof(T).GetProperties(flags);
            var targetList = dt.AsEnumerable().Select(dataRow =>
            {
                var instanceOfT = Activator.CreateInstance<T>();

                foreach (var properties in objectProperties.Where(properties => columnNames.Contains(properties.Name) && dataRow[properties.Name] != DBNull.Value))
                {
                    properties.SetValue(instanceOfT, dataRow[properties.Name], null);
                }
                return instanceOfT;
            }).ToList();

            return targetList;
        }
        public static void ExportToTextFile<T>(this IEnumerable<T> data, string FileName, char ColumnSeperator)
        {
            using (var sw = File.CreateText(FileName))
            {
                var plist = typeof(T).GetProperties().Where(p => p.CanRead && (p.PropertyType.IsValueType || p.PropertyType == typeof(string)) && p.GetIndexParameters().Length == 0).ToList();
                if (plist.Count > 0)
                {
                    var seperator = ColumnSeperator.ToString();
                    sw.WriteLine(string.Join(seperator, plist.Select(p => p.Name)));
                    foreach (var item in data)
                    {
                        var values = new List<object>();
                        foreach (var p in plist) values.Add(p.GetValue(item, null));
                        sw.WriteLine(string.Join(seperator, values));
                    }
                }
            }
        }
        public static byte[] ExportToExcel(DataTable dt)
        {
            try
            {
                var memoryStream = new MemoryStream();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var excelPackage = new ExcelPackage(memoryStream))
                {
                    var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                    worksheet.Cells["A1"].LoadFromDataTable(dt, true, TableStyles.None);
                    worksheet.Cells[1, 1, 1, dt.Columns.Count].Style.Font.Bold = true;
                    string lastAddress = worksheet.Dimension.Address.Last().ToString();

                    Color fontColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                    worksheet.Cells[1, 1, 1, dt.Columns.Count].Style.Font.Color.SetColor(fontColor);
                    Color backGroundColor = System.Drawing.ColorTranslator.FromHtml("#008738");
                    worksheet.Cells[1, 1, 1, dt.Columns.Count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, 1, 1, dt.Columns.Count].Style.Fill.BackgroundColor.SetColor(backGroundColor);


                    worksheet.DefaultRowHeight = 18;


                    worksheet.Column(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(6).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(7).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.DefaultColWidth = 20;
                    worksheet.Column(2).AutoFit();
                    return excelPackage.GetAsByteArray();
                }

            }
            catch (Exception)
            {
                throw;
            }

        }
        public static bool HasRowsInDataSetTable(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }
        public static string GetDBNUllString(object val)
        {
            if (val == DBNull.Value)
                return string.Empty;
            else
                return val.ToString();
        }
        public static bool GetDBNUllBoolean(string val)
        {
            if (string.IsNullOrEmpty(val))
                return false;
            else
            {
                switch (val.ToLower())
                {
                    case "true":
                        return true;
                    case "t":
                        return true;
                    case "1":
                        return true;
                    case "0":
                        return false;
                    case "false":
                        return false;
                    case "f":
                        return false;
                    default:
                        throw new InvalidCastException("You can't cast that value to a bool!");
                }
            }
        }
        public static int GetInteger(object val)
        {
            if (val == DBNull.Value)
                return 0;
            else
                return Convert.ToInt32(val);
        }
        public static DateTime GetDate(string val)
        {
            return Convert.ToDateTime(val);
        }
        public static decimal GetFloat(object val)
        {
            if (val == DBNull.Value)
                return 0;
            else
                return Convert.ToDecimal(val);
        }
        public static string GetDBNUllDateString(object val, string format)
        {
            if (val == DBNull.Value)
                return string.Empty;
            else
            {
                if (DateTime.TryParse(val.ToString(), out DateTime dateString))
                    return dateString.ToString(format);
                else
                    return string.Empty;
            }
        }
        public static string GetCurrentUserIPAddr()
        {
            string ipAddr;
            try
            {
                ipAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(ipAddr))
                {
                    if (ipAddr.IndexOf(",") > 0)
                    {
                        string[] ipRange = ipAddr.Split(',');
                        int le = ipRange.Length - 1;
                        ipAddr = ipRange[le];
                    }
                }
                else
                {
                    ipAddr = HttpContext.Current.Request.UserHostAddress;
                }
            }
            catch { ipAddr = null; }
            return ipAddr;
        }
        public static string DeviceName(string IP)
        {
            try
            {
                System.Net.IPAddress myIP = System.Net.IPAddress.Parse(IP);
                IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
                List<string> compName = GetIPHost.HostName.ToString().Split('.').ToList();
                if (string.IsNullOrEmpty(compName.ToString()))
                {
                    return "mypc";
                }
                else
                {
                    return compName.First();
                }
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
