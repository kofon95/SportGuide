using Dal;
using SportGuideASP.Core;
using SportGuideASP.Core.ViewModels;
using SportGuideASP.Properties;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static SportGuideASP.StaticData;

namespace SportGuideASP.Controllers
{
    public class UserController : Controller
    {
        DataManager _dm = new DataManager();

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
        public ActionResult SignIn(UserViewModel.SignIn login, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                Log.Warn("ModelState is not valid in SignIn. Email - " + login?.Email);
                return View();
            }

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = FormsAuthentication.DefaultUrl;
                Log.Debug("Not exists returnUrl");
            }


            var foundLogin = _dm.Login.GetAll().FirstOrDefault(t => t.email == login.Email);
            if (foundLogin == null)
            {
                Log.Debug("Login not exists. Email - " + login.Email);
                ModelState.AddModelError("", Resource.UserNotExists);
                return View();
            }

            string password = PasswordCryptoProvider.Encode(login.Password);
            if (password != foundLogin.password)
            {
                Log.Debug("Wrong password by email - " + login.Email);
                ModelState.AddModelError("", Resource.WrongEmail);
                return View();
            }

            AuthenticateUser(foundLogin);
            return Redirect(returnUrl);
        }

        private void AuthenticateUser(Login foundLogin)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1,
                foundLogin.email,
                DateTime.Now,
                DateTime.Now.Add(FormsAuthentication.Timeout),
                false,
                foundLogin.role);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            Response.Cookies.Add(cookie);

            Log.Debug("User authenticated - " + foundLogin.email);
        }

        public ActionResult SignOut()
        {
            Log.Trace("SignOut: " + User.Identity.Name);
            FormsAuthentication.SignOut();
            return Redirect(Request.UrlReferrer.AbsoluteUri ?? "/");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserViewModel.Register user)
        {
            if (!ModelState.IsValid)
            {
                Log.Warn("ModelState is not valid in SignIn. Email - " + user?.Email);
                return View();
            }

            if (_dm.Login.GetAll().FirstOrDefault(t => t.email == user.Email) != null)
            {
                ModelState.AddModelError("", Resource.UserExists);
                return View();
            }


            var login = _dm.Login.Save(new Login
            {
                email=user.Email,
                password=user.Password,
            });
            _dm.User.Save(new User
            {
                id= login.id,
                name = user.Name,
                first_ip = Request.ServerVariables["REMOTE_ADDR"],
            });

            AuthenticateUser(login);
            return Redirect(Request.UrlReferrer.AbsoluteUri ?? "/");
        }
    }
}