using BAL;
using DAL;
using DTO;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UINew.Controllers
{
    public class EmployeeController : BaseController
    {
        EmployeeBLL bll = new EmployeeBLL();
        #region Employee Information CRU Operation 
        // GET: Employee
        public ActionResult Index()
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            List<EmployeeVM> model = new List<EmployeeVM>();
            model = bll.GetEmployees(session);
            return View(model);
        }

        public List<SelectListItem> GenerateDateDropdown()
        {
            List<SelectListItem> dateList = new List<SelectListItem>();
            int currentYear = DateTime.Now.Year - 1;
            int yearRange = 17;
            for (int year = currentYear; year >= currentYear - yearRange; year--)
            {
                for (int month = 1; month <= 12; month++)
                {
                    string monthYear = new DateTime(year, month, 1).ToString("MMMM yyyy");
                    dateList.Add(new SelectListItem
                    {
                        Value = new DateTime(year, month, 1).ToString("yyyy-MMMM"),
                        Text = monthYear
                    });
                }
            }
            return dateList;
        }

        public ActionResult Create()
        {
            EmployeeVM model = new EmployeeVM();
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            model.DesignationType = SpecialBLL.GetDesignationForDdl();
            model.DistrictType = SpecialBLL.GetDistrictForDdl(session.DistId);
            model.BlockType = SpecialBLL.GetBlockForDdl(model.DistrictId);
            model.FacilityType = SpecialBLL.GetFacilityForDdl(model.BlockId);
            model.TrainingCompletionDates = GenerateDateDropdown();
            return View(model);
        }
        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(EmployeeVM model)
        {
            if (ModelState.IsValid)
            {
                // Check if the employee is trained
                if (model.Istrained)
                {
                    if (model.TrainingCompletionYear.HasValue)
                    {
                        // Get the selected year and month as a string (yyyy-MM)
                        var dateString = model.TrainingCompletionYear.Value.ToString("yyyy-MM");

                        // Parse it into a DateTime, appending "-01" to make it a valid date (e.g., yyyy-MM-01)
                        model.TrainingCompletionYear = DateTime.ParseExact(dateString + "-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Please select a valid training completion date.");
                        return View(model);
                    }
                }
                else
                {
                    model.TrainingCompletionYear = null;
                }
                SessionDTO session = (SessionDTO)Session["UserInfo"];
                bll.AddEmployeeInfo(model,session);
                TempData["SuccessMessage"] = "Added successfully.";
                TempData["Title"] = "SBA TRAINING.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["WarningMessage"] = "Error!! Please fill the neccessary area";
                TempData["Title"] = "SBA TRAINING.";
            }
            // Repopulate the date dropdown if validation fails
            model.TrainingCompletionDates = GenerateDateDropdown(); // Call the function to regenerate the dropdown
            model.DesignationType = SpecialBLL.GetDesignationForDdl();
            model.DistrictType = SpecialBLL.GetDistrictForDdl();
            model.BlockType = SpecialBLL.GetBlockForDdl();
            model.FacilityType = SpecialBLL.GetFacilityForDdl();
            return View(model);



        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            EmployeeVM model = new EmployeeVM();
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            model = bll.GetEmployeeInfoWithID(id,session);
            model.DesignationType = SpecialBLL.GetDesignationForDdl();
            model.DistrictType = SpecialBLL.GetDistrictForDdl();
            model.BlockType = SpecialBLL.GetBlockForDdl();
            model.FacilityType = SpecialBLL.GetFacilityForDdl();
            model.TrainingCompletionDates = GenerateDateDropdown();
            return View(model);
        }

        // POST: Employee/Edit/5
        //[HttpPost]
        //public ActionResult Edit(EmployeeVM model)
        //{
        //    SessionDTO session = (SessionDTO)Session["UserInfo"];
        //    IEnumerable<SelectListItem> designationlist = SpecialBLL.GetDesignationForDdl();
        //    IEnumerable<SelectListItem> districtlist = SpecialBLL.GetDistrictForDdl();
        //    IEnumerable<SelectListItem> blocklist = SpecialBLL.GetBlockForDdl();
        //    IEnumerable<SelectListItem> facilitylist = SpecialBLL.GetFacilityForDdl();
        //    IEnumerable<SelectListItem> trainingCompletionDates= GenerateDateDropdown();
        //    if (ModelState.IsValid)
        //    {
        //        model.DesignationType = SpecialBLL.GetDesignationForDdl();
        //        model.DistrictType = SpecialBLL.GetDistrictForDdl();
        //        model.BlockType = SpecialBLL.GetBlockForDdl();
        //        model.FacilityType = SpecialBLL.GetFacilityForDdl();
        //        model.TrainingCompletionDates = GenerateDateDropdown();                
        //        bll.UpdateEmployeeInfo(model,session);
        //        model.IsUpdate = true;
        //        TempData["SuccessMessage"] = "updated successfully.";
        //        TempData["Title"] = "SBA TRAINING.";
        //        return RedirectToAction("index");
        //    }
        //    else
        //        TempData["ErrorMessage"] = "Empty Area";
        //    TempData["Title"] = "Service Record Book.";
        //    model = bll.GetEmployeeInfoWithID(model.EmployeeId,session);
        //    model.DesignationType = designationlist;
        //    model.DistrictType = districtlist;
        //    model.BlockType = blocklist;
        //    model.FacilityType = facilitylist;
        //    model.TrainingCompletionDates = trainingCompletionDates;
        //    model.IsUpdate = true;
        //    return View(model);
        //}
        [HttpPost]
        public ActionResult Edit(EmployeeVM model)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];

            // Get dropdown lists
            IEnumerable<SelectListItem> designationlist = SpecialBLL.GetDesignationForDdl();
            IEnumerable<SelectListItem> districtlist = SpecialBLL.GetDistrictForDdl();
            IEnumerable<SelectListItem> blocklist = SpecialBLL.GetBlockForDdl();
            IEnumerable<SelectListItem> facilitylist = SpecialBLL.GetFacilityForDdl();
            IEnumerable<SelectListItem> trainingCompletionDates = GenerateDateDropdown();

            // Check if ModelState is valid
            if (!ModelState.IsValid)
            {
                // Log validation errors for debugging
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Debug.WriteLine(error.ErrorMessage); // Log or handle the error as needed
                }

                TempData["ErrorMessage"] = "Empty Area";
                TempData["Title"] = "Service Record Book.";

                // Re-populate dropdown lists before returning the view
                model.DesignationType = designationlist;
                model.DistrictType = districtlist;
                model.BlockType = blocklist;
                model.FacilityType = facilitylist;
                model.TrainingCompletionDates = trainingCompletionDates;
                model.IsUpdate = true;

                return View(model);
            }

            // If ModelState is valid, proceed to update the employee info
            bll.UpdateEmployeeInfo(model, session);
            model.IsUpdate = true;
            TempData["SuccessMessage"] = "Updated successfully.";
            TempData["Title"] = "SBA TRAINING.";

            return RedirectToAction("index");
        }

        public JsonResult DeleteEmployee(int ID)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            bll.DeleteEmployee(ID,session);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SBA Traning CRU Operation 
        // GET: Employee/EmpTrainingList
        public ActionResult EmpTrainingList()
        {
            List<TrainingVM> model = new List<TrainingVM>();
            model = bll.GetEmpTrainingList();
            return View(model);
        }
        //.Join(db.FacilityMasters, e => e.FacilityId, d => d.FacilityId, (e, d) => new { e, d })
        public JsonResult BindEmpTraining(int id)
        {
            using (shsbTrainingEntities db = new shsbTrainingEntities())
            {
                List<Employee_info> designationList = new List<Employee_info>();
                SessionDTO session = (SessionDTO)Session["UserInfo"];
                designationList = db.Employee_info.Where(e => e.DesignationId == id &&
                e.DistrictId==(session.DistId==-1?e.DistrictId:session.DistId) && !db.Employee_Training.Any(t => t.Employee_infoId == e.Id)
                && e.IsDeleted == false)
                    .OrderBy(x => x.AddDate).ToList();
                var dlist = designationList.Select(d => new { id = d.Id, name = d.Name + "-" + d.MobileNumber + "-" + d.FacilityId  });
               
                return Json(dlist, JsonRequestBehavior.AllowGet);
            }           

        }
        public ActionResult AddEmpTraining()
        {
            TrainingVM model = new TrainingVM();
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            model.DesignationType = SpecialBLL.GetDesignationForDdl();
            // model.EmployeeType = SpecialBLL.GetEmployeeInfoNameWithMobileForDdl(model.DesignationId,session.DistId);
            model.EmployeeType = SpecialBLL.GetEmployeeInfoNameWithMobileForDdl(model.DesignationId);
            model.MonthYearType = SpecialBLL.GetMonthYearForDdl();
            model.BatchType = SpecialBLL.GetBatchForDdl();
            return View(model);
        }

        // POST: Employee/AddEmpTraining
        [HttpPost]
        public ActionResult AddEmpTraining(TrainingVM model)
        {

            if (ModelState.IsValid)
            {
                //Select * from Employee_info where Id not in (Select Employee_infoId from Employee_Training) and IsDeleted = 0
                SessionDTO session = (SessionDTO)Session["UserInfo"];
                bll.AddEmpTraining(model,session);
                TempData["SuccessMessage"] = "Added successfully.";
                TempData["Title"] = "SBA TRAINING.";

                return RedirectToAction("EmpTrainingList");
            }
            else
                TempData["WarningMessage"] = "Error!! Please fill the neccessary area";
            TempData["Title"] = "SBA TRAINING.";
            model.DesignationType = SpecialBLL.GetDesignationForDdl();
            model.MonthYearType = SpecialBLL.GetMonthYearForDdl();
            model.EmployeeType = SpecialBLL.GetEmployeeInfoNameWithMobileForDdl(model.DesignationId);
            model.BatchType = SpecialBLL.GetBatchForDdl();
            return View(model);
        }

        // GET: Employee/UpdateEmpTraining/5
        public ActionResult UpdateEmpTraining(int id, SessionDTO session)
        {
            TrainingVM model = new TrainingVM();
            model = bll.GetEmployeeTrainingWithID(id,session);
            model.DesignationType = SpecialBLL.GetDesignationForDdl();
            model.MonthYearType = SpecialBLL.GetMonthYearForDdl();
            model.BatchType = SpecialBLL.GetBatchForDdl();
            model.EmployeeType = SpecialBLL.GetEmployeeInfoNameWithMobileForDdl(model.DesignationId);
            return View(model);
        }

        // POST: Employee/UpdateEmpTraining/5
        [HttpPost]
        public ActionResult UpdateEmpTraining(TrainingVM model)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            IEnumerable<SelectListItem> designationlist = SpecialBLL.GetDesignationForDdl();
            IEnumerable<SelectListItem> monthyearlist = SpecialBLL.GetMonthYearForDdl();
            IEnumerable<SelectListItem> empNameWithDesignationlist = SpecialBLL.GetEmployeeInfoNameWithMobileForDdl(model.DesignationId,session.DistId);
            IEnumerable<SelectListItem> batchlist = SpecialBLL.GetBatchForDdl();
            if (ModelState.IsValid)
            {
                model.DesignationType = SpecialBLL.GetDesignationForDdl();
                model.MonthYearType = SpecialBLL.GetMonthYearForDdl();
                model.EmployeeType = SpecialBLL.GetEmployeeInfoNameWithMobileForDdl(model.DesignationId);                
                bll.UpdateEmpTraining(model,session);
                TempData["SuccessMessage"] = "updated successfully.";
                TempData["Title"] = "SBA TRAINING.";
                return RedirectToAction("index");
            }
            else
                TempData["ErrorMessage"] = "Empty Area";
            TempData["Title"] = "Service Record Book.";
            model = bll.GetEmployeeTrainingWithID(model.Id,session);
            model.DesignationType = designationlist;
            model.MonthYearType = monthyearlist;
            model.EmployeeType = empNameWithDesignationlist;
            model.BatchType = batchlist;
            model.IsUpdate = true;
            return View(model);
        }

        public JsonResult DeleteEmployeeTraining(int ID)
        {
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            bll.DeleteEmployeeTraining(ID,session);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Bind Json    
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

        public JsonResult BindDesignation(int id)
        {
            using (shsbTrainingEntities db = new shsbTrainingEntities())
            {
                List<Employee_info> designationList = new List<Employee_info>();
                designationList = db.Employee_info.Where(x => x.DesignationId == id).ToList();
                var dlist = designationList.Select(d => new { id = d.Id, name = d.Name + "-" + d.MobileNumber });
                return Json(dlist, JsonRequestBehavior.AllowGet);
            }

        }

        #endregion

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            shsbTrainingEntities db = new shsbTrainingEntities();
            SessionDTO session = (SessionDTO)Session["UserInfo"];
            if (file != null && file.ContentLength > 0)
            {
                using (var package = new ExcelPackage(file.InputStream))
                {
                    var worksheet = package.Workbook.Worksheets.First();
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++) // Assuming row 1 is header
                    {
                        var employee = new tblEmployee
                        {
                            Id = Convert.ToInt32(worksheet.Cells[row, 1].Text),
                            Name = worksheet.Cells[row, 2].Text,
                            Position = worksheet.Cells[row, 3].Text,
                            Email = worksheet.Cells[row, 4].Text,
                            UserId = session.UserID

                        };

                        db.tblEmployees.Add(employee);
                    }

                    db.SaveChanges();
                }
                ViewBag.Message = "Upload successful!";
            }
            else
            {
                ViewBag.Message = "Please select a file.";
            }

            return View();
        }

    }
}