using BAL;
using CaptchaMvc.HtmlHelpers;
using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UINew.Models;

namespace UINew.Controllers
{
    public class HomeController : Controller
    {
        AccountBLL bll = new AccountBLL();
        // GET: Account
       
        public ActionResult Index()
        {
            LoginMasterDTO dto = new LoginMasterDTO();
            return View(dto);
        }
        [HttpPost]        
        public ActionResult Index(LoginMasterDTO model)
        {
            if (model.Username != null && model.Password != null)
            {

                LoginMasterDTO user = bll.GetUserWithUsernameAndPassword(model);

                if (!this.IsCaptchaValid(errorText: ""))
                {
                    TempData["ErrorMessage"] = "कृपया सही Captcha लिखे !!";
                    ViewBag.captcha = "कृपया सही Captcha लिखे !!";
                    return View(model);
                }
                if (user.UserId != 0)
                {
                    SessionDTO session = new SessionDTO();
                    session.UserID = user.UserId;
                    session.Namesurname = user.SurName;
                    session.Imagepath = user.Imagepath;
                    session.Email = user.Email;
                    session.Mobile = user.ContactNo;
                    session.Username = user.Username;
                    session.Role = user.RoleId.ToString();
                    session.RoleName = user.RoleName;
                    session.DistId = user.DistrictId;
                    //session.DesignationId = user.UserId;
                    //session.BlockId = user.BlockId;
                    //session.FacilityId = user.FacilityId;
                    HttpCookie cookie = new HttpCookie("Id");
                    cookie.Value = user.UserId.ToString();
                    FormsAuthentication.SetAuthCookie(user.UserId.ToString(), false);
                    Session.Add("UserInfo", session);
                    Session.Add("Id", user.UserId);

                    LogBLL.AddLog(General.ProcessType.Login, General.TableName.Login, 1, session);
                    TempData["SuccessMessage"] = "Logged IN Successfully";
                    TempData["Title"] = "SBA TRAINING";
                    return Redirect(FormsAuthentication.GetRedirectUrl(model.Username, false));
                }

                else
                {
                    TempData["loginerrormsge"] = "यूजर नाम और पासवर्ड की प्रविस्टी गलत है !! कृपया दोबारा प्रयास करे !! धन्यवाद !";
                    return View(model);
                }

            }
            else
                TempData["something"] = "कृपया पुन: प्रयास करें , कुछ गलत हो गया है |";
            return View(model);
        }

        // GET: Account/UserList
        [LoginControl]
        public ActionResult UserList()
        {
            List<LoginMasterDTO> model = new List<LoginMasterDTO>();
            model = bll.GetUsers();
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = ("Superadmin,Admin"))]
        public ActionResult AddUser()
        {
            LoginMasterDTO model = new LoginMasterDTO();
            model.RoleType = SpecialBLL.GetRolesForDdl();
            return View(model);
        }
        [HttpPost]
        [LoginControl]
        [Authorize(Roles = ("Superadmin,Admin"))]
        public ActionResult AddUser(LoginMasterDTO model)
        {
            if (model.UserImage == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }

            else if (ModelState.IsValid)
            {
                string filename = "";
                HttpPostedFileBase postedfile = model.UserImage;
                Bitmap UserImage = new Bitmap(postedfile.InputStream);
                Bitmap resizeImage = new Bitmap(UserImage, 128, 128);
                string ext = Path.GetExtension(postedfile.FileName);
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                {
                    string uniqueNumber = Guid.NewGuid().ToString();
                    filename = uniqueNumber + postedfile.FileName;
                    resizeImage.Save(Server.MapPath("~/Content/UserImage/" + filename));
                    model.Imagepath = filename;
                }
                else
                {
                    TempData["InfoMessage"] = "Error!! Please select file with true extension";
                    TempData["Title"] = "SBA TRAINING.";
                }
                model.RoleType = SpecialBLL.GetRolesForDdl();
                SessionDTO session = (SessionDTO)Session["UserInfo"];
                bll.AddUser(model,session);
                TempData["SuccessMessage"] = "Inserted successfully.";
                TempData["Title"] = "SBA TRAINING.";
                return RedirectToAction("userlist");
            }
            else
                TempData["WarningMessage"] = "Error!! Please fill the neccessary area";
                TempData["Title"] = "SBA TRAINING.";
            model.RoleType = SpecialBLL.GetRolesForDdl();
            return View(model);
        }
        [HttpGet]
        [LoginControl]
        [Authorize(Roles = ("Superadmin,Admin"))]
        public ActionResult UpdateUser(int ID,SessionDTO session)
        {
            LoginMasterDTO dto = new LoginMasterDTO();
            dto = bll.GetUserWithID(ID,session);
            dto.RoleType = SpecialBLL.GetRolesForDdl();
            return View(dto);
        }
        [HttpPost]
        [LoginControl]
        [Authorize(Roles = ("Superadmin,Admin"))]
        [ValidateInput(false)]
        public ActionResult UpdateUser(LoginMasterDTO model)
        {
            IEnumerable<SelectListItem> selectlist = SpecialBLL.GetRolesForDdl();

            SessionDTO session = (SessionDTO)Session["UserInfo"];
            if (ModelState.IsValid)
            {
                if (model.UserImage != null)
                {
                    string filename = "";
                    HttpPostedFileBase postedfile = model.UserImage;
                    Bitmap UserImage = new Bitmap(postedfile.InputStream);
                    Bitmap resizeImage = new Bitmap(UserImage, 128, 128);
                    string ext = Path.GetExtension(postedfile.FileName);
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                    {
                        string uniqueNumber = Guid.NewGuid().ToString();
                        filename = uniqueNumber + postedfile.FileName;
                        resizeImage.Save(Server.MapPath("~/Content/UserImage/" + filename));
                        model.Imagepath = filename;                       
                    }
                    else
                    {
                        TempData["WarningMessage"] = "Error!! Please select file with true extension";
                        TempData["Title"] = "Service Record Book.";
                    }
                }
                model.RoleType = SpecialBLL.GetRolesForDdl();
                string oldImagePath = bll.UpdateUser(model,session);

                if (model.UserImage != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/Content/UserImage/" + oldImagePath)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Content/UserImage/" + oldImagePath));
                    }
                }
                TempData["SuccessMessage"] = "updated successfully.";
                TempData["Title"] = "SBA TRAINING.";
                model.IsUpdate = true;
                return RedirectToAction("userlist");
            }
            else
                TempData["ErrorMessage"] = "Empty Area";
            TempData["Title"] = "SBA TRAINING.";
            model = bll.GetUserWithID(model.UserId,session);
            model.RoleType = selectlist;
            return View(model);
        }
        

        public JsonResult DeleteUser(int ID)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            string imagepath = bll.DeleteUser(ID,session);
            if (System.IO.File.Exists(Server.MapPath("~/Content/UserImage/" + imagepath)))
            {
                System.IO.File.Delete(Server.MapPath("~/Content/UserImage/" + imagepath));
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [LoginControl]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return Redirect("~/home/index");
        }
    }
}