using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ChequePrinting.UI.Controllers
{
    public abstract class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            if (session["UserId"] != null)
            {
                if (HttpContext.Session.IsNewSession == true)
                {
                    string sessionCookie = HttpContext.Request.Headers["Cookie"];

                    if ((sessionCookie != null) && (sessionCookie.IndexOf("ASP.NET_SessionId") >= 0))
                    {
                        if (!string.IsNullOrEmpty(HttpContext.Request.RawUrl))
                        {
                            filterContext.HttpContext.Response.Redirect(FormsAuthentication.LoginUrl);
                            return;
                        }

                    }
                }
            }
            else
            {
                //send them off to the login page
                var url = new UrlHelper(filterContext.RequestContext);
                var loginUrl = url.Content("~/Account/Login");

                filterContext.HttpContext.Response.Redirect(loginUrl, true);
            }
            base.OnActionExecuting(filterContext);
        }


        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    if (HttpContext.Session != null)
        //    {
        //        if (HttpContext.Session.IsNewSession == true)
        //        {
        //            string sessionCookie = HttpContext.Request.Headers["Cookie"];

        //            if ((sessionCookie != null) && (sessionCookie.IndexOf("ASP.NET_SessionId") >= 0))
        //            {
        //                if (!string.IsNullOrEmpty(HttpContext.Request.RawUrl) && HttpContext.Request.RawUrl != "/Account/Login")
        //                {
        //                    filterContext.HttpContext.Response.Redirect(FormsAuthentication.LoginUrl);
        //                    return;
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        //send them off to the login page
        //        var url = new UrlHelper(filterContext.RequestContext);
        //        var loginUrl = url.Content("~/Account/Login");

        //        filterContext.HttpContext.Response.Redirect(loginUrl, true);
        //    }
        //    base.OnActionExecuting(filterContext);
        //}
    }
}