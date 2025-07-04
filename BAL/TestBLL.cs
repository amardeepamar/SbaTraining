using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class TestBLL
    {
        TestDAO dao = new TestDAO();
        public bool AddTest(TestVM model, SessionDTO session)
        {
            Employee_info user = new Employee_info();

            user.Name = model.Name;
            user.MobileNumber = model.MobileNumber;
            user.DesignationId = model.DesignationId;
            user.DistrictId = model.DistrictId;
            user.BlockId = model.BlockId;
            user.FacilityId = model.FacilityId;
            // Ensure IsTrained and TrainingCompletionYear are passed correctly
            user.Istrained = model.Istrained;
            user.TrainingCompletionYear = Convert.ToDateTime(model.TrainingCompletionYear);
            user.AddDate = DateTime.Now;
            user.LastUpdateDate = DateTime.Now;
            user.UserId = session.UserID;
            int ID = dao.AddTest(user);
            LogDAO.AddLog(General.ProcessType.EmployeeInformationAdd, General.TableName.EmployeeInformation, ID, session);
            return true;
        }
        public static List<TestVM> SearchEmployees(int districtId, int blockId, int facilityId)
        {
            using (shsbTrainingEntities db = new shsbTrainingEntities())
            {
                var query = db.Employee_info.AsQueryable();

                if (districtId > 0)
                    query = query.Where(e => e.DistrictId == districtId);
                if (blockId > 0)
                    query = query.Where(e => e.BlockId == blockId);
                if (facilityId > 0)
                    query = query.Where(e => e.FacilityId == facilityId);
                return query
    .AsEnumerable() // Forces EF to execute query and switch to LINQ to Objects
    .Select(e => new TestVM
    {
        Name = e.Name,
        MobileNumber = e.MobileNumber,
        DesignationId = e.DesignationId,
        DistrictId = e.DistrictId,
        BlockId = e.BlockId,
        FacilityId = e.FacilityId,
        Istrained = e.Istrained ?? false, // if nullable
        TrainingCompletionYear = Convert.ToInt32(e.TrainingCompletionYear) // Safe now
    }).ToList();

            }
        }

    }
}
