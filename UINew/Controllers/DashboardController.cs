using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using static System.Collections.Specialized.BitVector32;
using System.Configuration;

namespace UINew.Controllers
{

    public class DashboardController : BaseController
    {
        // GET: Dashboard
        //[Authorize(Roles = ("Superadmin,Admin,Facility,Others,State,Users"))]
        shsbTrainingEntities _context = new shsbTrainingEntities();


        public int CountActiveUsers(TimeSpan activeDuration)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            DateTime threshold = DateTime.UtcNow.Subtract(activeDuration);
            return _context.LoginMasters.Where(x => x.IsDeleted == false).Count(x => x.AddDate >= threshold);
        }

        public ActionResult Index(int distId = -1)
        {
            using (shsbTrainingEntities db = new shsbTrainingEntities())
            {
                SessionDTO session = (SessionDTO)Session["UserInfo"];
                ViewData["TotalEmployee"] = db.Employee_info.Where(x => x.IsDeleted == false && x.DistrictId == (session.DistId.ToString() == "-1" ? x.DistrictId : session.DistId)).Count();




                ViewData["TotalTraining"] = (from lm in db.Employee_info.Where(x => x.IsDeleted == false && x.DistrictId == (session.DistId.ToString() == "-1" ? x.DistrictId : session.DistId))
                                             join ot in db.Employee_Training on lm.Id equals ot.Employee_infoId
                                             where lm.DistrictId == (session.DistId.ToString() == "-1" ? lm.DistrictId : session.DistId)
                                             select lm).Count();

                //db.Employee_Training.Where(x => x.IsDeleted == false).Count();
                ViewData["PageCount"] = db.Log_Table.Where(x => x.UserId == session.UserID).Count();
                ViewData["NoOfUsers"] = db.LoginMasters.Where(x => x.IsDeleted == false).Count();
                //int activeUserCount = CountActiveUsers(TimeSpan.FromMinutes(15));
                //ViewBag.ActiveUserCount = activeUserCount;

                // Define the time window for active users (e.g., 30 minutes)
                var timeWindow = DateTime.Now.AddMinutes(-30);

                // Query the database for users who have logged in within the last 30 minutes
                var activeUsersCount = _context.LoginMasters
                    .Where(u => u.LastUpdateDate >= timeWindow).GroupBy(u => u.Id)
                    .Count();

                // Pass the count to the view
                ViewBag.ActiveUsersCount = activeUsersCount;


                int totalDesignationWiseStaffNurse = GetTotalDesignationWiseStaffNurse();
                ViewData["TotalDesignationWiseStaffNurse"] = totalDesignationWiseStaffNurse;

                int totalDesignationWiseANM = GetTotalDesignationWiseANM();
                ViewData["TotalDesignationWiseANM"] = totalDesignationWiseANM;


                int totalTrainedStaffNurse = GetTrainedStaffNurse();
                ViewData["TotalTrainedStaffNurse"] = totalTrainedStaffNurse;

                int totalTrainedANM = GetTotalTrainedANM();
                ViewData["TotalTrainedANM"] = totalTrainedANM;


            }
            return View();
        }
        private int GetTotalDesignationWiseANM(int distId = -1)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            using (var db = new shsbTrainingEntities())
            {
                int totalDesignationWiseANMCount = db.Employee_info.Where(e => e.IsDeleted == false && e.DesignationId == 2 && e.DistrictId == (session.DistId.ToString() == "-1" ? e.DistrictId : session.DistId)).Count();                
                return totalDesignationWiseANMCount;
            }
        }
        private int GetTotalDesignationWiseStaffNurse(int distId = -1)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            using (var db = new shsbTrainingEntities())
            {
                int totalDesignationWiseStaffNurseCount = (db.Employee_info.Where(e => e.IsDeleted == false && e.DesignationId == 1 && e.DistrictId == (session.DistId.ToString() == "-1" ? e.DistrictId : session.DistId)).Count());
                // select Count(*) from Employee_info 
                // where IsDeleted = 0 and DesignationId = 1
                return totalDesignationWiseStaffNurseCount;
            }
        }

        private int GetTrainedStaffNurse(int distId = -1)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            using (var db = new shsbTrainingEntities())
            {
               // int totalTrainedStaffNurseCount = db.Employee_Training.Where(e => e.IsDeleted == false && e.DesignationId == 1 && e.DistrictId == (session.DistId.ToString() == "-1" ? e.DistrictId : session.DistId)).Count();
                 int totalTrainedStaffNurseCount = (from lm in db.Employee_info.Where(x => x.IsDeleted == false && x.DistrictId == (session.DistId.ToString() == "-1" ? x.DistrictId : session.DistId))
                          join ot in db.Employee_Training on lm.Id equals ot.Employee_infoId
                          where lm.DistrictId == (session.DistId.ToString() == "-1" ? lm.DistrictId : session.DistId) && ot.DesignationId==1
                          select lm).Count();
                return totalTrainedStaffNurseCount;
            }
        }

        private int GetTotalTrainedANM(int distId = -1)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            using (var db = new shsbTrainingEntities())
            {
                //int totalTrainedANMCount = db.Employee_Training.Where(e => e.IsDeleted == false && e.DesignationId == 2 && e.DistrictId == (session.DistId.ToString() == "-1" ? e.DistrictId : session.DistId)).Count();
                int totalTrainedANMCount = (from lm in db.Employee_info.Where(x => x.IsDeleted == false && x.DistrictId == (session.DistId.ToString() == "-1" ? x.DistrictId : session.DistId))
                                                   join ot in db.Employee_Training on lm.Id equals ot.Employee_infoId
                                                   where lm.DistrictId == (session.DistId.ToString() == "-1" ? lm.DistrictId : session.DistId) && ot.DesignationId == 2
                                                   select lm).Count();
                return totalTrainedANMCount;
            }
        }
    }
}
