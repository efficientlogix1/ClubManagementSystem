using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ClubBAIST.Models;
using System.Data;

namespace ClubBAIST.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        //ClubBAISTEntities db = new ClubBAISTEntities();

        //private ApplicationUserManager _userManager;
        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}
        // GET: User
        public ActionResult UserList()
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

        [HttpGet]
        public JsonResult Fetch()
        {
            //int userID = new GenericModel().FetchUserProfile().Id;
            //CallBackData callBackData = (new UserLogic()).Fetch(Request.FetchPaging(),userID);
            return Json(/*callBackData*/"", JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserSetup()
        {

            
            return View();
        }

        [AllowAnonymous]
        public ActionResult UserProfile()
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
        //public JsonResult FetchUserByID(int userID)
        //{
        //    return Json((new UserLogic()).FetchUserByID(userID), JsonRequestBehavior.AllowGet);
        //}

        
        //    public JsonResult FetchUserProfile()
        //{
        //    return Json((new UserLogic()).FetchUserByID((new GenericModel()).FetchUserProfile().Id), JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult Save(RegisterViewModel model)
        //{
        //    Message msg = new Message();
        //    model.RoleId = 2;
        //    msg.Action = "Save";
        //    if (ModelState.IsValid)
        //    {
        //        if (Request.Files != null)
        //        {
        //            HttpFileCollectionBase files = Request.Files;
        //            foreach (string file in files)
        //            {
        //                try
        //                {
        //                    HttpPostedFileBase postedFile = files[file];
        //                    string path = Server.MapPath("~/Assets/images/UserProfile");
        //                    if (model.Id != 0)
        //                    {
        //                        if (!String.IsNullOrEmpty(model.Photo))
        //                        {
        //                            if (System.IO.File.Exists(path + "/" + model.Photo))
        //                            {
        //                                System.IO.File.Delete(path + "/" + model.Photo);
        //                            }
        //                        }
        //                    }

        //                    model.Photo = model.Id + "_" + path.FetchUniquePath(postedFile.FileName);
        //                    postedFile.SaveAs(path + "/" + model.Photo);
        //                }
        //                catch (Exception ex)
        //                {
        //                    msg.Success = false;
        //                    msg.MessageDetail = ex.Message;
        //                    return Json(msg, JsonRequestBehavior.AllowGet);
        //                }
        //            }
        //        }
        //        if (model.Id == 0)
        //        {
        //            var user = new ApplicationUser { UserName = model.Username, Email = model.Email, Address = model.Address, CreatedTime = DateTime.Now, UpdatedTime = DateTime.Now, IsActive = model.IsActive, RoleId = model.RoleId, FirstName=model.FirstName,LastName=model.LastName,PhoneNumber=model.PhoneNumber,Photo=model.Photo };
        //            var result = UserManager.Create(user, model.Password);
        //            if (result.Succeeded)
        //            {
        //                model.Id = user.Id;
        //                //var foundUserRole = db.AspNetRoles.AsNoTracking().FirstOrDefault(u => u.Id == model.RoleId);
        //                //UserManager.AddToRole(user.Id, foundUserRole.Name);
        //                msg.MessageDetail = Message.SaveMessage;
        //                return Json(msg, JsonRequestBehavior.AllowGet);
        //            }
        //            msg.Success = false;
        //            foreach (var error in result.Errors)
        //            {
        //                msg.MessageDetail += error + "\n";
        //            }
        //            return Json(msg, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            //AspNetUser foundAspNetUser = db.AspNetUsers.FirstOrDefault(x => x.Id == model.Id);
                    
        //            //foundAspNetUser.PhoneNumber = model.PhoneNumber;
        //            //foundAspNetUser.Address = model.Address;
        //            //foundAspNetUser.FirstName = model.FirstName;
        //            //foundAspNetUser.LastName = model.LastName;
        //            //foundAspNetUser.IsActive = model.IsActive;
        //            //if (!string.IsNullOrEmpty(model.Photo))
        //            //{
        //            //    foundAspNetUser.Photo = model.Photo;
        //            //}
        //            //db.Entry(foundAspNetUser).State = System.Data.Entity.EntityState.Modified;
        //            //db.SaveChanges();
        //            msg.MessageDetail = Message.UpdateMessage;
        //            return Json(msg, JsonRequestBehavior.AllowGet);
        //        }

        //    }
        //    else
        //    {
        //        string errorstring = string.Empty;
        //        foreach (ModelState modelState in ViewData.ModelState.Values)
        //        {
        //            foreach (ModelError error in modelState.Errors)
        //            {
        //                errorstring += error.ErrorMessage + "\n";
        //            }
        //        }
        //        msg.Success = false;
        //        msg.MessageDetail = errorstring;
        //        return Json(msg, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public JsonResult SaveProfile(RegisterViewModel model)
        {
            Message msg = new Message();
            if (Request.Files != null)
            {
                HttpFileCollectionBase files = Request.Files;
                foreach (string file in files)
                {
                    try
                    {
                        HttpPostedFileBase postedFile = files[file];
                        string path = Server.MapPath("~/Assets/images/UserProfile");
                        if (model.Id != 0)
                        {
                            if (!String.IsNullOrEmpty(model.Photo))
                            {
                                if (System.IO.File.Exists(path + "/" + model.Photo))
                                {
                                    System.IO.File.Delete(path + "/" + model.Photo);
                                }
                            }
                        }

                        model.Photo = model.Id + "_" + path.FetchUniquePath(postedFile.FileName);
                        postedFile.SaveAs(path + "/" + model.Photo);
                    }
                    catch (Exception ex)
                    {
                        msg.Success = false;
                        msg.MessageDetail = ex.Message;
                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            //int userID = (new GenericModel()).FetchUserProfile().Id;
            //AspNetUser foundAspNetUser = db.AspNetUsers.FirstOrDefault(x => x.Id == userID);
            //foundAspNetUser.PasswordHash = (new PasswordHasher()).HashPassword(model.Password);
            //foundAspNetUser.ActualPassword = model.Password;
            //foundAspNetUser.CompanyID = model.CompanyID;
            //foundAspNetUser.PhoneNumber = model.PhoneNumber;
            //foundAspNetUser.Address = model.Address;
            //foundAspNetUser.FirstName = model.FirstName;
            //foundAspNetUser.LastName = model.LastName;
            //if (!string.IsNullOrEmpty(model.Photo))
            //{
            //    foundAspNetUser.Photo = model.Photo;
            //}
            //foundAspNetUser.Email = model.Email;
            //foundAspNetUser.UserName = model.Username;



            //db.Entry(foundAspNetUser).State = System.Data.Entity.EntityState.Modified;
            //db.SaveChanges();
            msg.MessageDetail = Message.UpdateMessage;
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


    }

}