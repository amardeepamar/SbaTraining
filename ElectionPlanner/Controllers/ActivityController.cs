using ElectionPlanner.Models.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectionPlanner.Controllers
{
    public class ActivityController : Controller
    {
      
        // GET: Activity
        public ActionResult Index()
        {            
            return View();
        }
    }
}