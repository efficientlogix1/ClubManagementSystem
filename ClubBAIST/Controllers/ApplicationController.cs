using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ClubBAIST.Controllers
{
    public class ApplicationController : Controller
    {
        // GET: Application
        public ActionResult Application()
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
        public ActionResult ApplicationView()
        {
            if (Request.Cookies["UserName"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpPost]
        public JsonResult Save(Application application)
        {
            return Json(new ApplicationLogic().Save(application), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ApplicationList()
        {
            if ((Request.Cookies["UserName"] != null))
            {
                if (Request.Cookies["Role"].Value == "MembershipCommittee")
                {
                    return View(new ApplicationLogic().GetApplicationList());
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
            
            //return View();
        }
        public ActionResult ApproveApplication()
        {
            if ((Request.Cookies["UserName"] != null))
            {
                if (Request.Cookies["Role"].Value == "MembershipCommittee")
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
        //public JsonResult DeleteApplication(int ApplicationID , Application model)
        //{
        //    return Json(new ApplicationLogic().DeleteApplication(ApplicationID, model), JsonRequestBehavior.AllowGet);
        //}
        [HttpGet]
        public JsonResult FetchApplicationByID(int applicationID)
        {
            return Json((new ApplicationLogic()).FetchApplicationByID(applicationID), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult BindDropdown()
        {
            return Json((new Generic()).FetchMembershipType(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteApplication(int ApplicationID)
        {
            return Json(new ApplicationLogic().DeleteApplication(ApplicationID), JsonRequestBehavior.AllowGet);
        }
    }
}