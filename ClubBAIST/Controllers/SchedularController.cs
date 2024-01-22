using BLL;
using BLL.SchedularLogic;
using DAL;
using DAL.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClubBAIST.Controllers
{
    public class SchedularController : Controller
    {
        // GET: Schedular
        public ActionResult Schedular()
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
        public ActionResult SchedularView()
        {
            if ((Request.Cookies["UserName"] != null))
            {
                return View();
                //if (Request.Cookies["Role"].Value == "ProShopStaff")
                //{
                //    return View();

                //}
                //else
                //{
                //    return RedirectToAction("Login", "Account");
                //}
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult GoldMember()
        {
            if ((Request.Cookies["UserName"] != null))
            {
                if (Request.Cookies["Role"].Value == "ProShopStaff" || Request.Cookies["Role"].Value == "Gold")
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
        public ActionResult SilverMember()
        {
            if ((Request.Cookies["UserName"] != null))
            {
                if (Request.Cookies["Role"].Value == "ProShopStaff" || Request.Cookies["Role"].Value == "Silver")
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

        public ActionResult BronzeMember()
        {
            if ((Request.Cookies["UserName"] != null))
            {
                if (Request.Cookies["Role"].Value == "ProShopStaff" || Request.Cookies["Role"].Value == "Bronze")
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

        public ActionResult CopperMember()
        {
            if ((Request.Cookies["UserName"] != null))
            {
                if (Request.Cookies["Role"].Value == "ProShopStaff" || Request.Cookies["Role"].Value == "Copper")
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

        public JsonResult AddSchedularView(TeeTime model)
        {
            model.MemberID = Convert.ToInt32(Request.Cookies["UserID"].Value);


            return Json(new SchedularLogic().Save(model), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SchedularList()
        {
            return Json(new SchedularLogic(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult BindDropdown()
        {
            return Json((new SchedularLogic()).GetEmployees(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult BindMembersDropdown()
        {
            return Json((new SchedularLogic()).GetMembers(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult BindMemberSchedule()
        {
            int memberID = Convert.ToInt32(Request.Cookies["UserID"].Value);
            string role = Request.Cookies["Role"].Value;
            return Json((new SchedularLogic()).FetchTeeTimeByMemberID(memberID,role), JsonRequestBehavior.AllowGet);
        }

        public JsonResult BindStaffSchedule()
        {
            string role = Request.Cookies["Role"].Value;
            return Json((new SchedularLogic()).FetchTeeTimeByStaff(role), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveStandingTeeTimeRequest(StandingTeeTimeRequest model)
        {
            model.MemberID = Convert.ToInt32(Request.Cookies["UserID"].Value);
            return Json((new SchedularLogic()).SaveStandingTeeTimeRequest(model), JsonRequestBehavior.AllowGet);
        }
        public JsonResult FetchTeeTimeByID(int recordID)
        {
           
            return Json((new SchedularLogic()).FetchTeeTimeByID(recordID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult FetchStandingTeeTimeRequestByID(int recordID)
        {

            return Json((new SchedularLogic()).FetchStandingTeeTimeRequestByID(recordID), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ClerkView()
        {
            if ((Request.Cookies["UserName"] != null))
            {
                if (Request.Cookies["Role"].Value == "Clerk")
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

        public JsonResult ConfirmStandingTeeTimeRequest(int recordID)
        {

            return Json(new SchedularLogic().ConfirmStandingTeeTimeRequest(recordID), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProShopStaffView()
        {
            if ((Request.Cookies["UserName"] != null))
            {
                if (Request.Cookies["Role"].Value == "ProShopStaff")
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
        public JsonResult DeleteTeeTime(int TeeTimeID)
        {
            return Json(new SchedularLogic().DeleteTeeTime(TeeTimeID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteStandingTeeTimeRequest(int StandingTeeTimeRequestID)
        {
            return Json(new SchedularLogic().DeleteStandingTeeTimeRequest(StandingTeeTimeRequestID), JsonRequestBehavior.AllowGet);
        }
        

    }
}