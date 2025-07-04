using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using ElectionPlanner.Models.DAL;
using ElectionPlanner.Models.DTO;

namespace UINew.Controllers
{

    public class DashboardController : BaseController
    {
        // GET: Dashboard
        //[Authorize(Roles = ("Superadmin,Admin,Facility,Others,State,Users"))]
        EpEntities _context = new EpEntities();


        public int CountActiveUsers(TimeSpan activeDuration)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            DateTime threshold = DateTime.UtcNow.Subtract(activeDuration);
            return _context.LoginMasters.Where(x => x.IsDeleted == false).Count(x => x.AddDate >= threshold);
        }

        public ActionResult Index()
        {
            using (EpEntities db = new EpEntities())
            {
                SessionDTO session = (SessionDTO)Session["UserInfo"];

                ViewData["PageCount"] = db.Log_Table.Where(x => x.UserId == session.UserID).Count();
                ViewData["NoOfUsers"] = db.LoginMasters.Where(x => x.IsDeleted == false).Count();

                // Define the time window for active users (e.g., 30 minutes)
                var timeWindow = DateTime.Now.AddMinutes(-30);

                // Query the database for users who have logged in within the last 30 minutes
                var activeUsersCount = _context.LoginMasters
                    .Where(u => u.LastUpdateDate >= timeWindow).GroupBy(u => u.Id)
                    .Count();

                // Pass the count to the view
                ViewBag.ActiveUsersCount = activeUsersCount;




            }
            return View();
        }
       
    }
}
