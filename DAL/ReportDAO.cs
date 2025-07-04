using DTO;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class ReportDAO: ShsTrainingContext
    {
        public List<EmployeeInfo_EmployeeTrainingVM> GetTrainingInfo(SessionDTO session)
        {
            List<EmployeeInfo_EmployeeTrainingVM> dtolist = new List<EmployeeInfo_EmployeeTrainingVM>();
            using (shsbTrainingEntities db = new shsbTrainingEntities())
            {
                var EmpInfoTraininglist = (from e in db.Employee_info.Where(x => x.IsDeleted == false && x.UserId==session.UserID)
                                join t in db.Employee_Training on e.Id equals t.Employee_infoId
                                select new
                                {
                                    Emp_Code = e.Id,
                                    Tra_Code = t.TrainingId,
                                    Emp_Name = e.Name,
                                    Mobile = e.MobileNumber,
                                    District = e.DistrictMaster.DistrictNameEn,
                                    Block = e.BlockMaster.BlockNameEn,
                                    Facility = e.FacilityMaster.FacilityName,
                                    Emp_Designation = e.DesignationMaster1.DesignationEn,
                                    Tra_Designation = t.DesignationMaster1.DesignationEn,
                                    Batch =t.tblBatchMaster.BatchName,
                                    Training_Period=e.TrainingCompletionYear,
                                    MonthYearPeriod=t.MonthYearMaster.MonthYearName,
                                    EmployeeNameOfTraining=e.Name + "," + e.MobileNumber + "," 
                                    + "(" + e.DistrictMaster.DistrictNameEn + "-" + e.BlockMaster.BlockNameEn + "-" + e.FacilityMaster.FacilityName + ")",
                                }).OrderBy(x => x.Emp_Code).ToList();

                foreach (var item in EmpInfoTraininglist)
                {
                    EmployeeInfo_EmployeeTrainingVM dto = new EmployeeInfo_EmployeeTrainingVM();
                    dto.Emp_Id = item.Emp_Code;
                    dto.Tra_Id = item.Tra_Code;
                    dto.EmployeeName = item.Emp_Name;
                    dto.Mobile = item.Mobile;
                    dto.District = item.District;
                    dto.Block = item.Block;
                    dto.Facility = item.Facility;
                    dto.Employee_Designation = item.Emp_Designation;
                    dto.Traning_Designation = item.Tra_Designation;
                    dto.Batch = item.Batch;
                    dto.Training_Completion_Year = item.Training_Period.ToString();
                    dto.Training_MonthYearName = item.MonthYearPeriod;
                    dto.EmployeeNameOfTraining = item.EmployeeNameOfTraining;
                    dtolist.Add(dto);
                }
            }
            return dtolist;
        }
    }
}
