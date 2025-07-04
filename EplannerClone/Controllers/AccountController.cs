using CaptchaMvc.HtmlHelpers;
using EplannerClone.CustomFilters;
using EplannerClone.ServiceLayer;
using EplannerClone.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EplannerClone.Controllers
{
    public class AccountController : Controller
    {
        IUsersService _us;
        IRolesServices _rs;
        public AccountController(IUsersService us,IRolesServices rs)
        {
            _us = us;
            _rs = rs;
        }
        // GET: Account
        [UserAuthorizationFilterAttribute]
        public ActionResult Index()
        {
            List<UserViewModel> users = _us.GetUsers().ToList();           
            return View(users);
        }
        public ActionResult Register()
        {
            UserViewModel uvm = new UserViewModel();
            uvm.Roles = _us.GetRoles(); // ✅ Fetch roles for dropdown
            return View(uvm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(UserViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                int uid = _us.InsertUser(rvm);
                Session["CurrentUserID"] = uid;
                Session["CurrentUserName"] = rvm.Username;
                Session["CurrentUserEmail"] = rvm.Email;
                Session["CurrentUserPassword"] = rvm.Password;

                Session["CurrentUserRoleID"] = rvm.RoleId;

               
                return RedirectToAction("index", "account");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                rvm.Roles = _us.GetRoles(); // Reload roles if form is invalid
                return View(rvm);
            }
        }
        public ActionResult Login()
        {           
            List<RoleViewModel> role = _rs.GetRoles();
            var model = new LoginViewModel
            {
                Role = role ?? new List<RoleViewModel>() // Ensure roles list is never null
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel lvm)
        {
            
            if (ModelState.IsValid)
            {
                UserViewModel uvm = _us.GetUsersByUsernameAndPassword(lvm.Username, lvm.Password,lvm.RoleId);
                if (!this.IsCaptchaValid(errorText: ""))
                {
                    TempData["ErrorMessage"] = "कृपया सही Captcha लिखे !!";
                    ViewBag.captcha = "कृपया सही Captcha लिखे !!";
                    return View(lvm);
                }
                if (uvm != null)
                {
                    Session["CurrentUserID"] = uvm.UserID;
                    Session["CurrentUserName"] = uvm.Username;
                    Session["CurrentUserEmail"] = uvm.Email;
                    Session["CurrentUserPassword"] = uvm.Password;
                    Session["CurrentUserContactNo"] = uvm.ContactNo;
                    Session["CurrentUserRoleID"] = uvm.RoleId;
                    if (uvm.RoleId == 1)
                    {
                        TempData["SuccessMessage"] = "Superadmin log-in Successfully";
                        TempData["Title"] = "ELECTION PLANNER";
                        return RedirectToRoute(new { controller = "Home", action = "Superadmin" });
                    }
                    else if (uvm.RoleId==2)
                    {
                        TempData["SuccessMessage"] = "Admin log-in Successfully";
                        TempData["Title"] = "ELECTION PLANNER";
                        return RedirectToRoute(new { controller = "Home", action = "Admin" });
                    }
                    else if (uvm.RoleId==3)
                    {
                        TempData["SuccessMessage"] = "General user log-in Successfully";
                        TempData["Title"] = "ELECTION PLANNER";
                        return RedirectToRoute(new { controller = "Home", action = "General" });
                    }
                    else
                    {
                        TempData["SuccessMessage"] = "Logged IN Successfully";
                        TempData["Title"] = "ELECTION PLANNER";
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Invalid Email / Password";
                    TempData["Title"] = "ELECTION PLANNER";
                    // ModelState.AddModelError("x", "Invalid Email / Password");
                    //lvm.Roles = _us.GetRoles(); 
                    
                    return View(lvm);
                }
            }
            else
            {
                TempData["WarningMessage"] = "Invalid Data";
                TempData["Title"] = "ELECTION PLANNER";
               // ModelState.AddModelError("x", "Invalid Data");
                return View(lvm);
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [UserAuthorizationFilterAttribute]
        public ActionResult ChangePassword()
        {
            int uid = Convert.ToInt32(Session["CurrentUserID"]);
            UserViewModel uvm = _us.GetUsersByUserID(uid);
            EditUserPasswordViewModel eupvm = new EditUserPasswordViewModel() { Email = uvm.Email, Password = "", ConfirmPassword = "", UserID = uvm.UserID };
            return View(eupvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilterAttribute]
        public ActionResult ChangePassword(EditUserPasswordViewModel eupvm)
        {
            if (ModelState.IsValid)
            {
                eupvm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                _us.UpdateUserPassword(eupvm);
                TempData["SuccessMessage"] = "Password change successfully.";
                TempData["Title"] = "Election Planner";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(eupvm);
            }
        }

        [UserAuthorizationFilterAttribute]
        public ActionResult ChangeProfile()
        {
            int uid = Convert.ToInt32(Session["CurrentUserID"]);
            UserViewModel uvm = _us.GetUsersByUserID(uid);
            EditUserDetailsViewModel eudvm = new EditUserDetailsViewModel() { Username = uvm.Username, Email = uvm.Email, ContactNo = uvm.ContactNo, UserID = uvm.UserID };
            return View(eudvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilterAttribute]
        public ActionResult ChangeProfile(EditUserDetailsViewModel eudvm)
        {
            if (ModelState.IsValid)
            {
                eudvm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                _us.UpdateUserDetails(eudvm);
                Session["CurrentUserName"] = eudvm.Username;
                return RedirectToAction("Superadmin", "home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(eudvm);
            }
        }


    }
}