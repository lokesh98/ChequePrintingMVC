using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequePrinting.BusinessLogicLayer.ViewModel
{
    public class ResponseViewModel
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public int LastId { get; set; }
    }
    public static class MsgBox
    {
        public static string save_msg = "Successfully Saved.";
        public static string file_upload_msg = "File uploaded successfully.";
        public static string update_msg = "Successfully Updated.";
        public static string delete_msg = "Successfully Deleted.";
        public static string batch_post = "Batch posted successfully.";
        public static string clear_msg = "Successfully Download Clear.";
        public static string post_msg = "Successfully Posted.";
        public static string id_not_found = "Id Not Found";
        public static string success_status = "success";
        public static string failed_status = "failed";
    }
}
