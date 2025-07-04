using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Web.Mvc;

namespace BAL
{
    public class EmployeeBLL
    {
        EmployeeDAO dao = new EmployeeDAO();

        public bool AddEmployeeInfo(EmployeeVM model,SessionDTO session)
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
            user.TrainingCompletionYear = model.TrainingCompletionYear;
            user.AddDate = DateTime.Now;
            user.LastUpdateDate = DateTime.Now;
            user.UserId = session.UserID;
            int ID = dao.AddEmployeeInfo(user);
            LogDAO.AddLog(General.ProcessType.EmployeeInformationAdd, General.TableName.EmployeeInformation, ID,session);
            return true;
        }

        public List<EmployeeVM> GetEmployees(SessionDTO session)
        {
            return dao.GetEmployees(session);
        }

        public EmployeeVM GetEmployeeInfoWithID(int id,SessionDTO session)
        {
            return dao.GetEmployeeInfoWithID(id,session);
        }

        public bool UpdateEmployeeInfo(EmployeeVM model,SessionDTO session)
        {
            dao.UpdateEmployeeInfo(model,session);
            LogDAO.AddLog(General.ProcessType.EmployeeInformationUpdate, General.TableName.EmployeeInformation, model.EmployeeId, session);
            return true;
        }

        public List<TrainingVM> GetEmpTrainingList()
        {
            return dao.GetEmpTrainingList();
        }

        public bool AddEmpTraining(TrainingVM model,SessionDTO session)
        {
  //select ID, (Name + ' ,' + MobileNumber) as [EmpName] from Employee_info Where DesignationId = 2 and
  //Id not in (Select Employee_infoId from Employee_Training) and IsDeleted = 0
            Employee_Training user = new Employee_Training();            
            user.MonthYearId = model.MonthYearId;
            user.DesignationId = model.DesignationId;
            user.BatchId = model.BatchId;
            user.Employee_infoId = model.Employee_infoId;           
            user.FromDate = model.FromDate;
            user.ToDate = model.ToDate;
            user.AddDate = DateTime.Now;
            user.LastUpdateDate = DateTime.Now;
            user.UserId = session.UserID;
            int ID = dao.AddEmpTraining(user);
            LogDAO.AddLog(General.ProcessType.EmployeeTrainingAdd, General.TableName.EmployeeTraining, ID, session);
            return true;
        }

        public TrainingVM GetEmployeeTrainingWithID(int id,SessionDTO session)
        {
            return dao.GetEmployeeTrainingWithID(id,session);
        }

        public bool UpdateEmpTraining(TrainingVM model,SessionDTO session)
        {
            dao.UpdateEmpTraining(model,session);
            LogDAO.AddLog(General.ProcessType.EmployeeTrainingUpdate, General.TableName.EmployeeTraining, model.Id, session);
            return true;
        }

        public void DeleteEmployee(int ID,SessionDTO session)
        {
            dao.DeleteEmployee(ID,session);
            LogDAO.AddLog(General.ProcessType.EmployeeInformationDelete, General.TableName.EmployeeInformation, ID, session);
        }

        public void DeleteEmployeeTraining(int ID,SessionDTO session)
        {
            dao.DeleteEmployeeTraining(ID,session);
            LogDAO.AddLog(General.ProcessType.EmployeeTrainingDelete, General.TableName.EmployeeTraining, ID, session);
        }
    }
}
