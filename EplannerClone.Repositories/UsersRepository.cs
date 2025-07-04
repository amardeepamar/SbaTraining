using EplannerClone.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EplannerClone.Repositories
{
    public interface IUsersRepository
    {
        void InsertUser(User u);
        List<Role> GetRoles(); // Change return type from SelectListItem to Role
        void UpdateUserDetails(User u);
        void UpdateUserPassword(User u);
        void DeleteUser(int uid);
        List<User> GetUsers();
        List<User> GetUsersByUsernameAndPassword(string Username, string Password);
        List<User> GetUsersByEmail(string Email);
        List<User> GetUsersByUserID(int UserID);
        int GetLatestUserID();
    }
    public class UsersRepository : IUsersRepository
    {
        EplannerDbDbContext db;

        public UsersRepository()
        {
            db = new EplannerDbDbContext();
        }

        public void InsertUser(User u)
        {
            db.Users.Add(u);
            db.SaveChanges();
        }
        public List<Role> GetRoles()
        {
            return db.Roles.ToList(); // Just return the Roles
        }
      
        public void UpdateUserDetails(User u)
        {
            User us = db.Users.Where(temp => temp.UserId == u.UserId).FirstOrDefault();
            if (us != null)
            {
                us.Username = u.Username;
                us.Email = u.Email;
                us.ContactNo = u.ContactNo;
                db.SaveChanges();
            }
        }

        public void UpdateUserPassword(User u)
        {
            User us = db.Users.Where(temp => temp.UserId == u.UserId).FirstOrDefault();
            if (us != null)
            {
                us.Password = u.Password;
                db.SaveChanges();
            }
        }

        public void DeleteUser(int uid)
        {
            User us = db.Users.Where(temp => temp.UserId == uid).FirstOrDefault();
            if (us != null)
            {
                db.Users.Remove(us);
                db.SaveChanges();
            }
        }

        public List<User> GetUsers()
        {
            List<User> us = db.Users.AsNoTracking().Where(x=>x.IsDeleted==false).OrderBy(temp => temp.Username).ToList();
            return us;
        }

        public List<User> GetUsersByUsernameAndPassword(string Username, string Password)
        {
            List<User> us = db.Users.Where(temp => temp.Username == Username && temp.Password == Password).ToList();
            
            return us;
        }

        public List<User> GetUsersByEmail(string Email)
        {
            List<User> us = db.Users.Where(temp => temp.Email == Email).ToList();
            return us;
        }

        public List<User> GetUsersByUserID(int UserID)
        {
            List<User> us = db.Users.Where(temp => temp.UserId == UserID).ToList();
            return us;
        }

        public int GetLatestUserID()
        {
            int uid = db.Users.Select(temp => temp.UserId).Max();
            return uid;
        }
    }
}
