using CopeyWinery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CopeyWinery.Controllers
{
    public class HomeController : Controller
    {
        private DB_Entities db = new DB_Entities();
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(User user)
        {
            RoleUser roleUser = db.ValidateUser(user.Username, user.Password).FirstOrDefault();

            string message = string.Empty;
            switch (roleUser.UserId.Value)
            {
                case -1:
                    message = "Username and/or password is incorrect.";
                    break;
                case -2:
                    message = "Account has not been activated.";
                    break;
                default:
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.Username, DateTime.Now, DateTime.Now.AddMinutes(2880), user.RememberMe, roleUser.Roles, FormsAuthentication.FormsCookiePath);
                    string hash = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

                    if (ticket.IsPersistent)
                    {
                        cookie.Expires = ticket.Expiration;
                    }
                    Response.Cookies.Add(cookie);
                    if (!string.IsNullOrEmpty(Request.Form["ReturnUrl"]))
                    {
                        return RedirectToAction(Request.Form["ReturnUrl"].Split('/')[2]);
                    }
                    else
                    {
                        return RedirectToAction("Profile");
                    }
            }

            ViewBag.Message = message;
            return View(user);
        }




        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }



    }

    

}