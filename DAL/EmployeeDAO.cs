using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace DAL
{
    public class EmployeeDAO : ShsTrainingContext
    {
        public int AddEmployeeInfo(Employee_info user)
        {
            try
            {
                db.Employee_info.Add(user);
                db.SaveChanges();
                return user.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddEmpTraining(Employee_Training user)
        {
            try
            {
                //db.Employee_info.Where(e => e.DesignationId == desiid
                //&& !db.Employee_Training.Any(t => t.Employee_infoId == e.Id) && e.IsDeleted == false).Select(e => new
                //{
                //    ID = e.Id,
                //    EmpName = e.Name + " ," + e.MobileNumber
                //}).ToList();
                db.Employee_Training.Add(user);
                db.SaveChanges();
                return user.TrainingId;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool DeleteEmployee(int ID, SessionDTO session)
        {
            Employee_info contact = db.Employee_info.First(x => x.Id == ID);
            contact.IsDeleted = true;
            contact.DeletedDate = DateTime.Now;
            contact.LastUpdateDate = DateTime.Now;
            contact.UserId = session.UserID;
            db.SaveChanges();
            return true;
        }

        public bool DeleteEmployeeTraining(int ID, SessionDTO session)
        {
            Employee_Training contact = db.Employee_Training.First(x => x.TrainingId == ID);
            contact.IsDeleted = true;
            contact.DeletedDate = DateTime.Now;
            contact.LastUpdateDate = DateTime.Now;
            contact.UserId = session.UserID;
            db.SaveChanges();
            return true;
        }

        public EmployeeVM GetEmployeeInfoWithID(int id, SessionDTO session)
        {
            Employee_info user = db.Employee_info.First(x => x.Id == id);
            EmployeeVM dto = new EmployeeVM();
            dto.EmployeeId = user.Id;
            dto.Name = user.Name;
            dto.MobileNumber = user.MobileNumber;
            dto.DesignationId = user.DesignationId;
            dto.DistrictId = user.DistrictId;
            dto.BlockId = user.BlockId;
            dto.FacilityId = user.FacilityId;
            user.UserId = session.UserID;
            user.LastUpdateDate = System.DateTime.Now;
            if (!dto.Istrained)
            {
                user.TrainingCompletionYear = null;
            }
            else
            {
                user.TrainingCompletionYear = dto.TrainingCompletionYear;
            }
            return dto;
        }

        public List<EmployeeVM> GetEmployees(SessionDTO session)
        {

            List<EmployeeVM> dtolist = new List<EmployeeVM>();
            using (shsbTrainingEntities db = new shsbTrainingEntities())
            {
                var userlist = (from lm in db.Employee_info.Where(x => x.IsDeleted == false && x.DistrictId== (session.DistId.ToString()=="-1"? x.DistrictId : session.DistId))
                                join ot in db.DesignationMasters on lm.DesignationId equals ot.DesignationId
                                join dist in db.DistrictMasters on lm.DistrictId equals dist.Id
                                join b in db.BlockMasters on lm.BlockId equals b.Id
                                join f in db.FacilityMasters on lm.FacilityId equals f.FacilityId
                                select lm).Where(a => a.DistrictId == (session.DistId.ToString() == "-1" ? a.DistrictId : session.DistId)).OrderByDescending(x => x.AddDate).ToList();
                foreach (var item in userlist)
                {
                    EmployeeVM dto = new EmployeeVM();
                    dto.Name = item.Name;
                    dto.EmployeeId = item.Id;
                    session.UserID = item.UserId;
                    dto.MobileNumber = item.MobileNumber;
                    dto.DesignationName = item.DesignationMaster1.DesignationEn;
                    dto.DistrictName = item.DistrictMaster.DistrictNameEn;
                    dto.BlockName = item.BlockMaster.BlockNameEn;
                    dto.FacilityName = item.FacilityMaster.FacilityName;
                    dto.Istrained = Convert.ToBoolean(item.Istrained);
                    dto.AddDate = item.AddDate.Date;
                    if (!dto.Istrained)
                    {
                        dto.TrainingCompletionYear = null;
                    }
                    else
                    {
                        dto.TrainingCompletionYear = (DateTime?)item.TrainingCompletionYear;
                    }

                    dtolist.Add(dto);
                }
            }
            return dtolist;
        }

        public TrainingVM GetEmployeeTrainingWithID(int id, SessionDTO session)
        {
            Employee_Training user = db.Employee_Training.First(x => x.TrainingId == id);
            TrainingVM dto = new TrainingVM();
            dto.Id = user.TrainingId;
            dto.MonthYearId = user.MonthYearId;
            dto.DesignationId = user.DesignationId;
            dto.Employee_infoId = user.Employee_infoId;
            dto.BatchId = (int)user.BatchId;
            dto.FromDate = (DateTime)user.FromDate;
            dto.ToDate = (DateTime)user.ToDate;
            user.UserId = session.UserID;
            user.LastUpdateDate = System.DateTime.Now;
            return dto;
        }

        public List<TrainingVM> GetEmpTrainingList()
        {
            List<TrainingVM> dtolist = new List<TrainingVM>();
            using (shsbTrainingEntities db = new shsbTrainingEntities())
            {
                var userlist = (from e in db.Employee_Training.Where(x => x.IsDeleted == false && x.FromDate != null)
                                join d in db.DesignationMasters on e.DesignationId equals d.DesignationId
                                join empinfo in db.Employee_info on e.Employee_infoId equals empinfo.Id
                                join myear in db.MonthYearMasters on e.MonthYearId equals myear.MonthYearId
                                join fac in db.FacilityMasters on empinfo.FacilityId  equals fac.FacilityId
                                select new
                                {
                                    TrainingNo = e.TrainingId,
                                    MonthYear = e.MonthYearMaster.MonthYearName,
                                    EmployeeName = e.Employee_info.Name + " - " + e.Employee_info.MobileNumber + " - " + e.DesignationMaster1.DesignationEn + " - " + fac.FacilityName ,
                                    AddedDate = e.AddDate,
                                    FromDate = (DateTime?)e.FromDate,
                                    ToDate = (DateTime?)e.ToDate,
                                    batch = e.tblBatchMaster.BatchName,
                                    EmployeeDistrict=empinfo.DistrictMaster.DistrictNameEn
                                }).OrderByDescending(x => x.AddedDate).ToList();

                foreach (var item in userlist)
                {
                    TrainingVM dto = new TrainingVM();
                    dto.Id = item.TrainingNo;
                    dto.MonthYearName = item.MonthYear;
                    dto.EmployeeName = item.EmployeeName;
                    dto.AddDate = item.AddedDate;
                    dto.FromDate = (DateTime)item.FromDate;
                    dto.ToDate = (DateTime)item.ToDate;
                    dto.BatchName = item.batch;
                    dto.EmployeeDist = item.EmployeeDistrict;
                    dtolist.Add(dto);
                }
            }
            return dtolist;

        }

        public void UpdateEmployeeInfo(EmployeeVM model, SessionDTO session)
        {
            try
            {
                Employee_info user = db.Employee_info.First(x => x.Id == model.EmployeeId);
                user.Name = model.Name;
                user.MobileNumber = model.MobileNumber;
                user.DesignationId = model.DesignationId;
                user.DistrictId = model.DistrictId;
                user.BlockId = model.BlockId;
                user.FacilityId = model.FacilityId;
                user.LastUpdateDate = DateTime.Now;
                user.UserId = session.UserID;
                user.Istrained = model.Istrained;
                user.TrainingCompletionYear = model.TrainingCompletionYear;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void UpdateEmpTraining(TrainingVM model, SessionDTO session)
        {
            try
            {
                Employee_Training user = db.Employee_Training.First(x => x.TrainingId == model.Id);
                user.MonthYearId = model.MonthYearId;
                user.DesignationId = model.DesignationId;
                user.Employee_infoId = model.Employee_infoId;
                user.BatchId = model.BatchId;
                model.FromDate =(DateTime) user.FromDate;
                model.ToDate =(DateTime) user.ToDate;
                user.LastUpdateDate = DateTime.Now;
                user.UserId = session.UserID;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
