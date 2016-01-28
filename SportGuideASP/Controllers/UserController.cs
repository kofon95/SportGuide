using Dal;
using SportGuideASP.Core;
using SportGuideASP.Core.Private;
using SportGuideASP.Core.Util;
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
        private const string _defaultRole = "User";
        DataManager _dm = new DataManager();

        // GET: User
        public ActionResult Index()
        {
            return RedirectToAction(nameof(Register));
        }
        [HttpGet]
        public ActionResult SignIn()
        {
            ViewBag.VK_ApiId = VK.AppId;

            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(UserViewModel.SignIn login)
        {
            if (!ModelState.IsValid)
            {
                Log.Warn("ModelState is not valid in SignIn. Email - " + login?.Email);
                return View();
            }


            var foundLogin = (
                    from l in _dm.Login.GetAll()
                    join u in _dm.User.GetAll() on l.id equals u.id
                    where l.email == login.Email
                    select new { Login = l, Role = u.role }
                ).FirstOrDefault();

            if (foundLogin == null)
            {
                Log.Debug("Login not exists. Email - " + login.Email);
                ModelState.AddModelError("", Resource.UserNotExists);
                return View();
            }

            string password = PasswordIncoder.Encode(login.Password);
            if (password != foundLogin.Login.password)
            {
                Log.Debug("Wrong password by email - " + login.Email);
                ModelState.AddModelError("", Resource.WrongEmail);
                return View();
            }

            AuthenticateUser(foundLogin.Login.id, foundLogin.Role.Replace(" ", ""));
            return Redirect(ReturnUrl);
        }

        public ActionResult AuthVK(UserViewModel.UserVK vkLogin)
        {
            #region Check cookie

            try
            {
                string cookie = Request.Cookies[VK.CookieName]?.Value;
                if (!VK.CookieIsValid(cookie))
                    throw new ValueOfCookieException("Wrong cookie's value: \"" + cookie + "\"");
            }
            catch (ValueOfCookieException e)
            {
                Log.Error(e.Message);
                return Redirect("/error.html#" + e.Message);
            }

            #endregion

            var user = _dm.LoginSocialNetwork.GetAll()
                .FirstOrDefault(t => t.network_name == "VK" &&
                                t.uid_sn == vkLogin.uid);
            if (user == null)
            {
                // Registration
                var registeredUser = Register_VK(vkLogin);
                AuthenticateUser(registeredUser.id, registeredUser.role);
            }
            else
            {
                // Log in
                AuthenticateUser(user.id, _defaultRole);
            }

            return Redirect(FormsAuthentication.DefaultUrl);
        }
        private User Register_VK(UserViewModel.UserVK vkLogin)
        {
            var savedUser = _dm.User.Save(new User
            {
                name = vkLogin.first_name + " " + vkLogin.last_name,
                gender = vkLogin.gender,
                birthday = vkLogin.bdate,
                photo_src = vkLogin.photo_scr,
                photo_is_local = false,
                role = _defaultRole,
                first_ip = IpAddress,
            });

            var savedLogin = _dm.LoginSocialNetwork.Save(new LoginSocialNetwork
            {
                id = savedUser.id,
                network_name = "VK",
                uid_sn = vkLogin.uid,
            });

            return savedUser;
        }

        private void AuthenticateUser(int userId, string roles)
        {
            string userIdStr = userId.ToString();
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1,
                userIdStr,
                DateTime.Now,
                DateTime.Now.Add(FormsAuthentication.Timeout),
                false,
                roles);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            Response.Cookies.Add(cookie);

            Log.Trace("User authenticated - " + userIdStr);
        }

        public ActionResult SignOut()
        {
            Log.Trace("SignOut: " + User.Identity.Name);
            FormsAuthentication.SignOut();

            var referer = Request.UrlReferrer;
            if (referer?.LocalPath?.StartsWith("/Admin") as bool? == false)
                return Redirect(referer?.AbsoluteUri);
            return Redirect("/");
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
                Log.Trace("Such user already exists - " + user.Email);
                return View();
            }


            var saveUser = _dm.User.Save(new User
            {
                name = user.Name,
                role = _defaultRole,
                first_ip = IpAddress,
            });
            var login = _dm.Login.Save(new Login
            {
                id = saveUser.id,
                email = user.Email,
                password = user.Password,
            });

            AuthenticateUser(login.id, saveUser.role);

            return Redirect(ReturnUrl);
        }
        
        private string IpAddress { get { return Request.ServerVariables["REMOTE_ADDR"]; } }

        public string ReturnUrl
        {
            get
            {
                var referrer = Request.UrlReferrer;
                var url = Request.Url;

                return (referrer != null &&
                        referrer.Authority == url.Authority &&
                        referrer.AbsoluteUri != url.AbsoluteUri)
                            ?
                            referrer.AbsoluteUri :
                            FormsAuthentication.DefaultUrl;
            }
        }
    }
}