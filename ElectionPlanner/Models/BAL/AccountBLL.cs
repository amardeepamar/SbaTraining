
using ElectionPlanner.Models.DAL;
using ElectionPlanner.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionPlanner.Models.BAL
{
    public class AccountBLL
    {
        AccountDAO dao = new AccountDAO();
        public LoginMasterDTO GetUserWithUsernameAndPassword(LoginMasterDTO model)
        {
            LoginMasterDTO dto = new LoginMasterDTO();
            dto = dao.GetUserWithUsernameAndPassword(model);
            return dto;
        }

        public List<LoginMasterDTO> GetUsers()
        {
            return dao.GetUsers();
        }

        public bool AddUser(LoginMasterDTO model, SessionDTO session)
        {
            LoginMaster user = new LoginMaster();
            user.Username = model.Username;
            user.Password = model.Password;
            user.ContactNo = model.ContactNo;
            user.Password.Equals(model.ConfirmPassword);
            user.Email = model.Email;
            user.ImagePath = model.Imagepath;
            user.NameSurname = model.SurName;
            user.AddDate = DateTime.Now;
            user.LastUpdateDate = DateTime.Now;
            user.LastUpdateUserID = session.UserID;
            user.RoleId = model.RoleId;
            user.DistId = model.DistrictId;
            user.AcId = model.AcId;
            user.IsDeleted = false;
            user.StateId = 1;
            int ID = dao.AddUser(user);
            LogDAO.AddLog(General.ProcessType.UserAdd, General.TableName.Users, ID,session);
            return true;
        }

        public LoginMasterDTO GetUserWithID(int ID,SessionDTO session)
        {
            return dao.GetUserWithID(ID,session);
        }

        public string UpdateUser(LoginMasterDTO model,SessionDTO session)
        {
            string oldImagePath = dao.UpdateUser(model,session);
            LogDAO.AddLog(General.ProcessType.UserUpdate, General.TableName.Users, model.UserId,session);
            return oldImagePath;
        }

        public string DeleteUser(int ID,SessionDTO session)
        {
            string imagepath = dao.DeleteUser(ID,session);
            LogDAO.AddLog(General.ProcessType.UserDelete, General.TableName.Users, ID, session);
            return imagepath;
        }
    }
}
