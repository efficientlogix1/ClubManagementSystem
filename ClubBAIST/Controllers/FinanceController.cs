using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClubBAIST.Controllers
{
    public class FinanceController : Controller
    {
        // GET: Finance
        public ActionResult Finance()
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

        public ActionResult FinanceMemberAccount()
        {
            if ((Request.Cookies["UserName"] != null))
            {
                if (Request.Cookies["Role"].Value == "FinanceCommittee")
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
    }
}