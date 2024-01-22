using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClubBAIST.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult Member()
        {
            if ((Request.Cookies["UserName"] != null))
            {
                if (Request.Cookies["UserType"].Value == "Member")
                {
                    return View();

                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult MemberAccount()
        {
            if ((Request.Cookies["UserName"] != null))
            {
                if (Request.Cookies["UserType"].Value == "Member")
                {
                    return View();

                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public JsonResult Save(MemberViewModel memberViewModel)
        {
            return Json(new MemberLogic().Save(memberViewModel), JsonRequestBehavior.AllowGet);
        }

    }
}