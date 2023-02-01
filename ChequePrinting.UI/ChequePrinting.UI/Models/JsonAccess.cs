using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChequePrinting.UI.Models
{
    public static class JsonAccess
    {
        public static bool Fails()
        {
            if (HttpContext.Current.Session["UserID"] == null)
                return true;
            else
                return false;
        }
        public static string GetUser()
        {
            if (HttpContext.Current.Session["UserID"] != null)
                return HttpContext.Current.Session["UserID"].ToString();
            else
                return string.Empty;
        }
        public static string UserCode()
        {
            if (HttpContext.Current.Session["UserID"] != null)
                return HttpContext.Current.Session["UserID"].ToString();
            else
                return string.Empty;
        }
        public static string UserName()
        {
            if (HttpContext.Current.Session["UserName"] != null)
                return HttpContext.Current.Session["UserName"].ToString();
            else
                return string.Empty;
        }
        public static string UserRole()
        {
            if (HttpContext.Current.Session["UserRoleName"] == null)
                return "";
            string userRole = HttpContext.Current.Session["UserRoleName"].ToString();
            return userRole;
        }
        public static bool IsUserAdmin()
        {
            if (HttpContext.Current.Session["UserRoleName"] == null)
                return false;
            string userRole = HttpContext.Current.Session["UserRoleName"].ToString();
            if (userRole == "ADMIN")
                return true;

            return false;
        }
    }
}