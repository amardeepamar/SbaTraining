using ElectionPlanner.Models.BAL;
using ElectionPlanner.Models.DAL;
using ElectionPlanner.Models.DTO;
using ElectionPlanner.Models.DTO.Ddl;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Web.Mvc;
using UINew.Controllers;

namespace ElectionPlanner.Controllers
{
    public class ElectionPlannerController : BaseController
    {
        private readonly PollDayBLL pollDayBLL = new PollDayBLL();
        private readonly PollDaySubLayerBLL pollDaySubLayerBLL = new PollDaySubLayerBLL();        
        private readonly SpecialBLL bll = new SpecialBLL();
        // GET: ElectionPlanner
        
        #region PollDay Add Update and List  

        [HttpGet]
        public ActionResult AddOrUpdatePollDay()
        {
            // Fetch all PollDay
            var pollday = pollDayBLL.GetPollDay();
            ViewBag.pollday = pollday;
            return View(pollday);
        }
        [HttpPost]
        public ActionResult AddOrUpdatePollDay(PollDayDTO pollday)
        {
            if (pollday.Id == 0)
            {
                if (!string.IsNullOrEmpty(pollday.PollDayNameEN))
                {
                    // Add new PollDay                
                    if (pollDayBLL.AddPollDay(pollday))
                    {
                        TempData["SuccessMessage"] = "Poll day added successfully!";
                    }
                    else
                    {

                        TempData["ErrorMessage"] = "Failed to add poll day.";
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(pollday.PollDayNameEN)) { TempData["ErrorMessage"] = "Poll day name is required!!"; }
                    return RedirectToAction("AddOrUpdatePollDay");
                }
            }
            else
            {
                // Update existing PollDay
                if (pollDayBLL.UpdatePollDay(pollday))
                {
                    TempData["SuccessMessage"] = "Poll day updated successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to update Poll day.";
                }
            }

            return RedirectToAction("AddOrUpdatePollDay");
        }
        #endregion

        #region PollDay SubLayer Add Update and List  


        [HttpGet]
        public ActionResult AddOrUpdatePollDaySubLayer()
        {
            // Fetch all PollDay
            var polldaySubLayer = pollDaySubLayerBLL.GetPollDaySubLayer();
            ViewBag.pollday = pollDayBLL.GetPollDay();
            return View(polldaySubLayer);
        }
        [HttpPost]
        public ActionResult AddOrUpdatePollDaySubLayer(PollDaySubLayerDTO polldaySubLayer)
        {
            if (polldaySubLayer.Id == 0)
            {
                if (!string.IsNullOrEmpty(polldaySubLayer.PollDaySubLayerNameEN))
                {
                    // Add new PollDay                
                    if (pollDaySubLayerBLL.AddPollDaySubLayer(polldaySubLayer))
                    {
                        TempData["SuccessMessage"] = "Poll day Sub Layer added successfully!";
                    }
                    else
                    {

                        TempData["ErrorMessage"] = "Failed to add poll day Sub Layer.";
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(polldaySubLayer.PollDaySubLayerNameEN)) { TempData["ErrorMessage"] = "Poll day Sub Layer name is required!!"; }
                    return RedirectToAction("AddOrUpdatePollDaySubLayer");
                }
            }
            else
            {
                // Update existing PollDay
                if (pollDaySubLayerBLL.UpdatePollDaySubLayer(polldaySubLayer))
                {
                    TempData["SuccessMessage"] = "Poll day Sub Layer updated successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to update Poll day Sub Layer.";
                }
            }

            return RedirectToAction("AddOrUpdatePollDaySubLayer");
        }
        #endregion

        [HttpGet]
        public JsonResult GetAcByDistrict(int distId)
        {
            
            var acList = pollDayBLL.GetAcByDistrict(distId); // Call BAL method
            return Json(acList, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public JsonResult GetActivityByAc(int districtId, int acId) // 1 time
        //{

        //    var activity = pollDayBLL.GetActivityByAc(districtId, acId);
        //    return Json(activity, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        public JsonResult GetActivityDetailsByAc(int district, int ac) //2 time
        {
            var details = pollDayBLL.GetActivityDetailsByAc(district, ac);
            return Json(details, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddOrUpdateActivities()
        {
            var activites = pollDaySubLayerBLL.GetActivities();
            ViewBag.RemarkWithActivity = pollDayBLL.GetRemarksWithActivitiesJoin();
            ViewBag.remarks = pollDayBLL.GetRemarks();
            ViewBag.Districts = pollDayBLL.GetAllDistricts();
            return View(activites);
        }

        [HttpPost]
        public JsonResult AddOrUpdateActivities(ActivitiesDTO activity)
        {
            bool result = pollDaySubLayerBLL.SaveActivity(activity);
            return Json(new { success = result });
        }


    }
}