using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClubBAIST.Controllers
{
    public class StaffController : Controller
    {
        // GET: Staff
        public ActionResult Staff()
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
        public ActionResult StaffSetup()
        {
            if ((Request.Cookies["UserName"] != null))
            {

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public JsonResult Save(StaffViewModel staffViewModel )
        {
            return Json(new StaffLogic().Save(staffViewModel), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult StaffList()
        {
            return View(new StaffLogic().GetStaffList());
            //return View();
        }

        [HttpGet]
        public JsonResult FetchStaffByID(int employeeID)
        {
            return Json((new StaffLogic()).FetchStaffByID(employeeID), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult BindDropdown()
        {
            return Json((new Generic()).FetchStaffType(), JsonRequestBehavior.AllowGet);
        }
    }
}