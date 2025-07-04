using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EplannerClone.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Superadmin()
        {
            return View();
        }
        public ActionResult Admin() 
        {  
            return View();
        }
        public ActionResult General()
        {
            return View();
        }
    }
}