using BAuth.DAL;
using BAuth.Hellpers;
using Facebook;
using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BAuth.Controllers
{


    public class LoginController : BaseController
    {
        [HttpGet]
        [Route("вход")]
        public ActionResult Login(string ReturnUrl)
        {
            return View("Login");
        }

        [HttpPost]
        [Route("вход")]
        public ActionResult Login(string Email, string Password, string ReturnUrl)
        {

            string hashPassword = GetHash(Password);

            User user = null;
            using (BAuthDBContext dc = new BAuthDBContext())
            {
                user = dc.Users.Where(u => u.Email == Email && u.password == hashPassword).SingleOrDefault();
            }

            if (user == null)
            {
                ViewBag.error = "Грешно потребителско име или парола";
                return View("Login");
            }

           

            Session["user"] = user;

            FormsAuthenticationTicket authTicket =
                new FormsAuthenticationTicket(1, user.UserId, DateTime.Now, DateTime.Now.AddMinutes(200), true, user.Role, "/");
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                                               FormsAuthentication.Encrypt(authTicket));
            Response.Cookies.Add(cookie);
            return Redirect(ReturnUrl);

        }

        [Route("изход")]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Remove("user");
            return Redirect("/");
        }

        [AllowAnonymous]
        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["appId"],
                client_secret = ConfigurationManager.AppSettings["appSecret"],
                redirect_uri = RediredtUri.AbsoluteUri,
                response_type = "code",
                scope = "email"
            });
            return Redirect(loginUrl.AbsoluteUri);
        }


      
               
        private Uri RediredtUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;

            }
        }


    }
}