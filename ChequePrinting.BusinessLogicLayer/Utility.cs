using ChequePrinting.BusinessLogicLayer.ViewModel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ChequePrinting.BusinessLogicLayer
{
    public static class Utility
    {
        public static DataTable ToDataTable<T>(this IQueryable items)
        {
            Type type = typeof(T);

            var props = TypeDescriptor.GetProperties(type)
                                      .Cast<PropertyDescriptor>()
                                      .Where(propertyInfo => propertyInfo.PropertyType.Namespace.Equals("System"))
                                      .Where(propertyInfo => propertyInfo.IsReadOnly == false)
                                      .ToArray();

            var table = new DataTable();

            foreach (var propertyInfo in props)
            {
                table.Columns.Add(propertyInfo.Name, Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType);
            }

            foreach (var item in items)
            {
                table.Rows.Add(props.Select(property => property.GetValue(item)).ToArray());
            }

            return table;
        }
       
        public static byte[] ExportToExcel(DataTable dt)
        {
            try
            {
                var memoryStream = new MemoryStream();
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
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
        public static DataTable GetDataFromXLSFile(string filePath)
        {
            string connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", filePath);
            var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
            var ds = new DataSet();
            adapter.Fill(ds, "ExcelTable");
            return ds.Tables[0];
        }
       
        public static string ConvertObjectToXMLString(object classObject)
        {
            string xmlString = null;
            XmlSerializer xmlSerializer = new XmlSerializer(classObject.GetType());
            using (MemoryStream memoryStream = new MemoryStream())
            {
                xmlSerializer.Serialize(memoryStream, classObject);
                memoryStream.Position = 0;
                xmlString = new StreamReader(memoryStream).ReadToEnd();
            }
            return xmlString;
        }

        public static DataTable GetAuditLogRptDT(IEnumerable<AuditLogViewModel> items)
        {

            DataTable dt = new DataTable();

            dt.Columns.Add("IAM Admin ID");
            dt.Columns.Add("Buesiness User Bank/LoginID");
            dt.Columns.Add("Request Type");
            dt.Columns.Add("Old Value");
            dt.Columns.Add("New Value");
            dt.Columns.Add("Action Date");
            dt.Columns.Add("Action Time");

            foreach (var item in items)
            {
                DataRow dr = dt.NewRow();
                dr["IAM Admin ID"] = item.UserId;
                dr["Buesiness User Bank/LoginID"] = item.ReflectedUserID;
                dr["Request Type"] = item.ActionPerformed;
                dr["Old Value"] = item.OldValue;
                dr["New Value"] = item.NewValue;
                dr["Action Date"] = item.TimeStamp;
                dr["Action Time"] = item.TimeSpan;
                dt.Rows.Add(dr);
            }

            return dt;
        }
    }
}
