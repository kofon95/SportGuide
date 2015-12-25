using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportGuideASP.Core.ViewEntities;
using System.Web.Security;
using NLog;
using static SportGuideASP.StaticData;

namespace SportGuideASP.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(User.SignIn user, string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = FormsAuthentication.DefaultUrl;
                Log.Debug("Not exists returnUrl");
            }



            // select from db

            string role = "Admin"; // change admin
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1,
                user.Email,
                DateTime.Now,
                DateTime.Now.Add(FormsAuthentication.Timeout),
                false,
                role);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            Response.Cookies.Add(cookie);

            int id = 1;
            Log.Debug("User authenticated: " + user.Email + " / " + id);
            return Redirect(returnUrl);
        }

        [HttpPost]
        public ActionResult SignOut()
        {
            Log.Trace("SignOut: " + User.Identity.Name);
            FormsAuthentication.SignOut();
            return Redirect(Request.UrlReferrer.AbsoluteUri ?? "/");
        }
    }
}