using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLL;
using DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ClubBAIST.Models;
using DAL.DBModels;
using System.Web.Security;
using System.Collections.Generic;
using System.Data;

namespace ClubBAIST.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            LoggedInUser loginModel = new LoggedInUser();
            loginModel.Username = model.Username;
            loginModel.Password = model.Password;
            var user = (new AccountLogic()).UserLogin(loginModel);
            if (!string.IsNullOrEmpty(user.UserType))
            {

                setCookie(user);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid Username or bad credential.";
                return View();

            }

        }


        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (ForgotPasswordSendEmail(model))
                {
                    return RedirectToAction("ForgotPasswordConfirmation", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Email not valid");
                    return View(model);
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        private bool ForgotPasswordSendEmail(ForgotPasswordViewModel model)
        {
            try
            {
                UserViewModel user = new UserViewModel();
                
                List<DbParameters> parameters = new List<DbParameters>();
                string query = "[dbo].[FetchEmail]";
                parameters.Add(new DbParameters { ParamName = "@Email", ParamValues = model.Email });                
                DataTable dt = SqlHelper.FilDataSet(query, true, parameters).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    user.FirstName = dt.Rows[0]["FirstName"].ToString();
                    user.LastName = dt.Rows[0]["LastName"].ToString();
                    user.Username = dt.Rows[0]["Username"].ToString();
                    user.Password = dt.Rows[0]["Password"].ToString();

                }
               
                //Fetch username and password
                if (!string.IsNullOrEmpty(user.Username))
                {
                    string emailBody = "Hi '"+user.FirstName+" "+user.LastName+"' !<br />it seems that you have forgotten your following credentials to login on ClubBaist App.";
                    emailBody += "<br />Username: "+user.Username;
                    emailBody += "<br />Password: "+user.Password;
                    string subject = "Forgot Password";
                    return (new Generic()).SendEmail(emailBody,subject,model.Email);
                }
                else
                    return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }


        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        

        public ActionResult LogOff()
        {
            Session.Abandon();
            Session.Clear();
            Response.Cookies["Username"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["UserType"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["Role"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["UserID"].Expires = DateTime.Now.AddDays(-1);
            
            return RedirectToAction("Login");
        }



        public void setCookie(LoggedInUser User)
        {            
            Response.Cookies["Username"].Expires = DateTime.Now.AddDays(260);
            Response.Cookies["Username"].Value = User.Username;
            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(260);
            Response.Cookies["Password"].Value = User.Password;
            Response.Cookies["UserType"].Expires = DateTime.Now.AddDays(260);
            Response.Cookies["UserType"].Value = User.UserType;
            Response.Cookies["Role"].Expires = DateTime.Now.AddDays(260);
            Response.Cookies["Role"].Value = User.Role;
            if (User.UserType== "Member")
            {
                Response.Cookies["UserID"].Expires = DateTime.Now.AddDays(260);
                Response.Cookies["UserID"].Value = User.MemberID.ToString();
            }
            else
            {
                Response.Cookies["UserID"].Expires = DateTime.Now.AddDays(260);
                Response.Cookies["UserID"].Value = User.EmployeeID.ToString();
            }
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion



    }
}