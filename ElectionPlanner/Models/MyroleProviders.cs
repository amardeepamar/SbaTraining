using ElectionPlanner.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Security;

namespace ElectionPlanner.Models
{
    public class MyroleProviders : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string[] GetRolesForUser(string username)
        {
            using (var context = new EpEntities())
            {
                var res = (from user in context.LoginMasters
                           join role in context.RoleMasters
                           on user.RoleId equals role.Id
                           where user.Username == username  // 🔴 Use Username instead of RoleId
                           select role.RoleName).ToArray();
                return res;
            }

        }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (var context = new EpEntities())
            {
                return (from user in context.LoginMasters
                        join role in context.RoleMasters on user.RoleId equals role.Id
                        where user.Username == username && role.RoleName == roleName
                        select user).Any();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        


        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}