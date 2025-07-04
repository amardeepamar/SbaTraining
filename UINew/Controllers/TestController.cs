using BAL;
using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UINew.Controllers
{
    public class TestController : BaseController
    {
        TestBLL bll = new TestBLL();
       
        // GET: Test
        public ActionResult Index()
        {
            TestVM model = new TestVM(); 
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            model.DistrictType = SpecialBLL.GetDistrictForDdl(session.DistId);
            model.BlockType = SpecialBLL.GetBlockForDdl(model.DistrictId);
            model.FacilityType = SpecialBLL.GetFacilityForDdl(model.BlockId);
          //  model.EmployeeList = new List<Employee_info>(); // or null is fine too

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(TestVM model)
        {
            if (ModelState.IsValid)
            {
                SessionDTO session = (SessionDTO)Session["UserInfo"];
                bll.AddTest(model, session);

                TempData["SuccessMessage"] = "Added successfully.";
                TempData["Title"] = "SBA TRAINING.";

                // Repopulate dropdowns
                model.DistrictType = SpecialBLL.GetDistrictForDdl();
                model.BlockType = SpecialBLL.GetBlockForDdl();
                model.FacilityType = SpecialBLL.GetFacilityForDdl();

                // Load employee list after save
                model.EmployeeList = TestBLL.SearchEmployees(model.DistrictId, model.BlockId, model.FacilityId);

                return View(model);
            }

            // Validation failed – repopulate dropdowns and load EmployeeList too
            model.DistrictType = SpecialBLL.GetDistrictForDdl();
            model.BlockType = SpecialBLL.GetBlockForDdl();
            model.FacilityType = SpecialBLL.GetFacilityForDdl();

            model.EmployeeList = TestBLL.SearchEmployees(model.DistrictId, model.BlockId, model.FacilityId);

            TempData["WarningMessage"] = "Error!! Please fill the necessary area";
            TempData["Title"] = "SBA TRAINING.";

            return View(model);
        }

        //[HttpPost]
        //public ActionResult Index(TestVM model)
        //{
        //    try
        //    {
        //        // You can skip ModelState.IsValid if it's just a search form
        //        // But if you want to keep it, make sure dropdown values are required
        //        if (ModelState.IsValid)
        //        {
        //            // Only perform search logic, NOT add/insert
        //            model.EmployeeList = TestBLL.SearchEmployees(model.DistrictId, model.BlockId, model.FacilityId);

        //            TempData["SuccessMessage"] = "Search completed successfully.";
        //            TempData["Title"] = "SBA TRAINING.";
        //        }
        //        else
        //        {
        //            TempData["WarningMessage"] = "Error!! Please fill the necessary area";
        //            TempData["Title"] = "SBA TRAINING.";
        //        }

        //        // Repopulate dropdowns
        //        model.DistrictType = SpecialBLL.GetDistrictForDdl();
        //        model.BlockType = SpecialBLL.GetBlockForDdl();
        //        model.FacilityType = SpecialBLL.GetFacilityForDdl();

        //        return View(model);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log exception here
        //        TempData["WarningMessage"] = "Internal error: " + ex.Message;
        //    }

        //}


        public JsonResult BindBlock(int id)
        {
            using (shsbTrainingEntities db = new shsbTrainingEntities())
            {
                List<BlockMaster> blockList = new List<BlockMaster>();
                blockList = db.BlockMasters.Where(x => x.DistrictId == id).ToList();
                var blist = blockList.Select(b => new { id = b.Id, name = b.BlockNameEn });
                return Json(blist, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult BindFacility(int id)
        {
            using (shsbTrainingEntities db = new shsbTrainingEntities())
            {
                List<FacilityMaster> blockList = new List<FacilityMaster>();
                blockList = db.FacilityMasters.Where(x => x.BlockID == id).ToList();
                var blist = blockList.Select(b => new { id = b.FacilityId, name = b.FacilityName });
                return Json(blist, JsonRequestBehavior.AllowGet);
            }
        }

        

    }
}