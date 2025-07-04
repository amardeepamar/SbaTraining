
using ElectionPlanner.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionPlanner.Models.DAL
{
    public class AccountDAO:EpContext
    {
        public LoginMasterDTO GetUserWithUsernameAndPassword(LoginMasterDTO model)
        {
            SessionDTO session=new SessionDTO();
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
                model.AcId = (int)user.AcId;
            }
            return model;
        }
        public List<LoginMasterDTO> GetUsers()
        {
            SessionDTO session = new SessionDTO();
            List<LoginMasterDTO> dtolist = new List<LoginMasterDTO>();
            using (EpEntities db = new EpEntities())
            {
                var userlist = (from u in db.LoginMasters.Where(x => x.IsDeleted == false)
                                join r in db.RoleMasters on u.RoleId equals r.Id
                                join d in db.DistrictMasters on u.DistId equals d.Id
                                join a in db.tblAcs on u.AcId equals a.AC_NO
                                select new
                                {
                                    UserId = u.Id,
                                    ContactNo = u.ContactNo,
                                    Email = u.Email,
                                    NameSurname = u.NameSurname,
                                    AddDate = u.AddDate,
                                    RoleName = r.RoleName,
                                    RoleId=u.RoleId,
                                    Username = u.Username,
                                    Password=u.Password,
                                    DistrictName=d.DistrictNameHn,
                                    Acname=a.AC_NAME_HN
                                }).OrderBy(x => x.AddDate).ToList();

                foreach (var item in userlist)
                {
                    LoginMasterDTO dto = new LoginMasterDTO();
                    dto.ContactNo = item.ContactNo;
                    dto.SurName = item.NameSurname;
                    dto.UserId = item.UserId;
                    dto.Email = item.Email;
                    dto.RoleId = item.RoleId;
                    dto.RoleName = item.RoleName;
                    dto.Username = item.Username;
                    dto.Password = item.Password;
                    dto.DistrictName = item.DistrictName;
                    dto.AcName = item.Acname;
                    dtolist.Add(dto);
                }
            }
            return dtolist;
        }

        public LoginMasterDTO GetUserWithID(int ID,SessionDTO session)
        {
            LoginMaster user = db.LoginMasters.First(x => x.Id == ID);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
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
            dto.DistrictId =(int) user.DistId;
            dto.AcId=(int) user.AcId;
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
                if (user == null)
                {
                    throw new Exception($"User with ID {model.UserId} not found.");
                }

                string oldImagePath = user.ImagePath ?? "No previous image"; // Handle null case

                Console.WriteLine($"Old Image Path: {oldImagePath}"); // Debugging

                user.NameSurname = model.SurName;
                user.Username = model.Username;

                // Ensure ImagePath is only updated if a new one is provided                
                if (!string.IsNullOrEmpty(model.Imagepath))
                {
                    user.ImagePath = model.Imagepath;
                }
                else if (string.IsNullOrEmpty(user.ImagePath))
                {
                    user.ImagePath = "346d270c-6dd6-4ff6-b382-8babf2dd3b5b0f2fdf60-35f7-4729-99e8-cbd657543e90CS.png"; // Set a default image if missing
                }

                user.Email = model.Email;
                user.Password = model.Password;
                user.Password = model.ConfirmPassword;
                user.ContactNo = model.ContactNo;
                user.LastUpdateDate = DateTime.Now;
                user.LastUpdateUserID = session.UserID;
                user.RoleId = model.RoleId;
                user.DistId = model.DistrictId;
                user.AcId = model.AcId;
                user.StateId = 1;
                db.SaveChanges();
                return oldImagePath;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message} | StackTrace: {ex.StackTrace}");
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
