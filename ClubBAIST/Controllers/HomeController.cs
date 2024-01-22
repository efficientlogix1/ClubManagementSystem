using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClubBAIST.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.Cookies["UserName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult LandingPage()
        {
            if (Request.Cookies["UserName"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}