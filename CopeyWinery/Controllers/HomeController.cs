using CopeyWinery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CopeyWinery.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Your application home page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(User userLogin)
        {
            LoginHandler loginHandler = new LoginHandler(userLogin);
            var user = loginHandler.GetUser();
            return View(user);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User userLogin)
        {
            LoginHandler loginHandler = new LoginHandler(userLogin);
            var user = loginHandler.GetUser();
            //if (user != null)
            //{
            //    if (user.Is_Admin == true)
            //    {
                    return RedirectToAction("Index");
            //    }
            //    else {
            //        return RedirectToAction("UserDashBoard");
            //    }

            //}
            //return View(user);
        }

        public ActionResult UserDashBoard()
        {
            //if (Session["ID"] != null)
            //{
            return View();
            //}
            //else
            //{
            //  return RedirectToAction("Login");
            //}
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

        public ActionResult AdminDashBoard()
        {
            var obj = new List<User>();

            using (DB_Entities db = new DB_Entities())
            {
                obj = db.Users.ToList();
                //if (obj != null)
                //{

                //    //return RedirectToAction("AdminDashboard");
                //}
            }

            return View(obj);
        }
        

    }

    

}