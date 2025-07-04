using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AccountDAO:ShsTrainingContext
    {
        public LoginMasterDTO GetUserWithUsernameAndPassword(LoginMasterDTO model)
        {
            LoginMaster user = db.LoginMasters.FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);
            if (user != null && user.Id != 0)
            {
                model.UserId = user.Id;
                model.Username = user.Username;
                model.SurName = user.NameSurname;
                model.Email = user.Email;
                model.ContactNo = user.ContactNo;
                model.Imagepath = user.ImagePath;
                model.RoleId = user.RoleId;
                model.RoleName = user.RoleMaster.RoleName;
                model.DistrictId =(int) user.DistId;
              
                //model.BlockId =(int) user.BlockId;
                //model.FacilityId =(int) user.FacilityId;
            }
            return model;
        }
        public List<LoginMasterDTO> GetUsers()
        {
            List<LoginMasterDTO> dtolist = new List<LoginMasterDTO>();
            using (shsbTrainingEntities db = new shsbTrainingEntities())
            {
                var userlist = (from lm in db.LoginMasters.Where(x => x.IsDeleted == false)
                                join ot in db.RoleMasters on lm.RoleId equals ot.Id
                                select new
                                {
                                    UserId = lm.Id,
                                    ContactNo = lm.ContactNo,
                                    Email = lm.Email,
                                    NameSurname = lm.NameSurname,
                                    AddDate = lm.AddDate,
                                    RoleName = ot.RoleName,
                                    Username = lm.Username,
                                    Password=lm.Password
                                }).OrderBy(x => x.AddDate).ToList();

                foreach (var item in userlist)
                {
                    LoginMasterDTO dto = new LoginMasterDTO();
                    dto.ContactNo = item.ContactNo;
                    dto.SurName = item.NameSurname;
                    dto.UserId = item.UserId;
                    dto.Email = item.Email;
                    dto.RoleName = item.RoleName;
                    dto.Username = item.Username;
                    dto.Password = item.Password;
                    dtolist.Add(dto);
                }
            }
            return dtolist;
        }

        public LoginMasterDTO GetUserWithID(int ID,SessionDTO session)
        {
            LoginMaster user = db.LoginMasters.First(x => x.Id == ID);
            LoginMasterDTO dto = new LoginMasterDTO();
            dto.UserId = user.Id;
            dto.SurName = user.NameSurname;
            dto.Username = user.Username;
            dto.Password = user.Password;
            dto.Email = user.Email;
            dto.Imagepath = user.ImagePath;
            dto.ContactNo = user.ContactNo;
            dto.ConfirmPassword = user.Password;
            dto.RoleId = user.RoleId;
            session.DistId =(int) user.DistId;
            return dto;
        }

        public string DeleteUser(int ID, SessionDTO session)
        {
            try
            {
                LoginMaster user = db.LoginMasters.First(x => x.Id == ID);
                user.IsDeleted = true;
                user.DeletedDate = DateTime.Now;
                user.LastUpdateDate = DateTime.Now;
                user.LastUpdateUserID = session.UserID;
                db.SaveChanges();
                return user.ImagePath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UpdateUser(LoginMasterDTO model,SessionDTO session)
        {
            try
            {
                LoginMaster user = db.LoginMasters.First(x => x.Id == model.UserId);
                string oldImagePath = user.ImagePath;
                user.NameSurname = model.SurName;
                user.Username = model.Username;
                if (model.Imagepath != null)
                    user.ImagePath = model.Imagepath;
                user.Email = model.Email;
                user.Password = model.Password;
                user.Password = model.ConfirmPassword;
                user.ContactNo = model.ContactNo;
                user.LastUpdateDate = DateTime.Now;
                user.LastUpdateUserID = session.UserID;
                user.RoleId = model.RoleId;
                user.DistId = session.DistId;
                db.SaveChanges();
                return oldImagePath;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int AddUser(LoginMaster user)
        {
            try
            {
                db.LoginMasters.Add(user);
                db.SaveChanges();
                return user.Id;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
