using ChequePrinting.BusinessLogicLayer;
using ChequePrinting.BusinessLogicLayer.Repository.AccountRepository;
using ChequePrinting.BusinessLogicLayer.Repository.AuditLogRepository;
using ChequePrinting.BusinessLogicLayer.Repository.LoggerRepository;
using ChequePrinting.BusinessLogicLayer.ViewModel;
using ChequePrinting.UI.Models;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ChequePrinting.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger logger;
        private readonly IAccountRepository accountRepository;
        private readonly IAuditLogRepository auditLogRepository;
        public AccountController(ILogger _logger, IAccountRepository _accountRepository, IAuditLogRepository _auditLogRepository)
        {
            this.accountRepository = _accountRepository;
            this.auditLogRepository = _auditLogRepository;
            this.logger = _logger;
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (Session["UserId"] != null)
            {
                return this.RedirectToLocal(returnUrl);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string userId, string password, bool rememberMe)
        {
            JsonOutput json = new JsonOutput();
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    if (accountRepository.ValidateUser(userId))
                    {
                        var user = accountRepository.GetUserByUserID(userId);
                        if (user.IsLocked == true)
                        {
                            json.success = false;
                            json.message = "Maximum Attempt Reached (3 times) .Your Account is locked.";
                            auditLogRepository.AddAuditLog(new AuditLogViewModel() { UserId = user.UserID, ActionPerformed = "Login", ActionDesc = "User Locked", Url = Request.Url.ToString() });
                            return Json(json, JsonRequestBehavior.AllowGet);
                        }
                        else if (user.IsLoggedIn)
                        {
                            json.success = false;
                            json.message = "Already Loged in on another System, please Logout";
                            return Json(json, JsonRequestBehavior.AllowGet);
                        }
                        var ldap = LdapAuthentication(userId, password);
                        if (ldap)
                        {
                            FormsAuthentication.SetAuthCookie(user.UserID, rememberMe);
                            Session["UserID"] = user.UserID.ToString();
                            Session["UserName"] = user.UserName.ToString();
                            Session["UserRoleID"] = user.RoleID.ToString();
                            Session["UserRoleName"] = user.RoleName.ToString();                           
                            json.message = "Logged in successfully";
                            
                            auditLogRepository.AddAuditLog(new AuditLogViewModel() { UserId = user.UserID, ActionPerformed = "Login", ActionDesc = json.message, Url = Request.Url.ToString() });
                            accountRepository.UpdateLoginInfo(user.UserID, "LOGGED_IN", 0);
                            json.result = user.LandingPageUrl;
                            json.success = true;
                        }
                        else
                        {
                            accountRepository.UpdateLoginInfo(userId, "INVALID_CREDENTIAL", user.LoginFailedCount);
                            json.success = false;
                            json.message = "Invalid UserID or password provided";
                        }
                    }
                    else
                    {
                        json.message = "User Not Registered in System";
                        json.success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                json.message = ex.Message.ToString();
                json.success = false;
            }

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LogOff()
        {
            accountRepository.UpdateLoginInfo(JsonAccess.UserCode(), "LOG_OFF", 0);
            auditLogRepository.AddAuditLog(new AuditLogViewModel() { UserId = JsonAccess.GetUser(), ReflectedUserID=JsonAccess.GetUser(), ActionPerformed = "LogOff", ActionDesc = "User Logged off from system", Url = Request.Url.ToString() });

            Session["UserID"] = null;
            Session["UserName"] = null;
            Session["UserRoleID"] = null;
            Session["UserRoleName"] = null;

            Session.Clear();
            Response.Cookies.Clear();
            Session.RemoveAll();
            Session.Abandon();
            FormsAuthentication.SignOut();
            HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            // Clear authentication cookie
            HttpCookie rFormsCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "")
            {
                Expires = DateTime.Now.AddYears(-1)
            };
            Response.Cookies.Add(rFormsCookie);

            // Clear session cookie 
            HttpCookie rSessionCookie = new HttpCookie("ASP.NET_SessionId", "")
            {
                Expires = DateTime.Now.AddYears(-1)
            };
            Response.Cookies.Add(rSessionCookie);

            return RedirectToAction("Login", "Account");
        }
        public bool LdapAuthentication(string userId, string password)
        {
            bool res;
            if (LDAPSettings.IsTesting)
            {
                res = true;
            }
            else
            {
                try
                {
                    var client = new LDAPClient(userId, LDAPSettings.LDAPDomain, password, LDAPSettings.LDAPUrl);
                    if (client.ValidateUserByBind(userId, password))
                    {
                        res = true;
                    }
                    else
                    {
                        res = false;
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex);
                    res = false;
                }
            }
            return res;
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            try
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return this.RedirectToAction("Index", "Home");
        }
    }
}