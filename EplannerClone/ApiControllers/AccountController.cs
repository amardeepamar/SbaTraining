using EplannerClone.ServiceLayer;
using EplannerClone.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace EplannerClone.ApiControllers
{
    public class AccountController : ApiController
    {
        IUsersService us;

        public AccountController(IUsersService us)
        {
            this.us = us;
        }

        public string Get(string Email)
        {
            if (this.us.GetUsersByEmail(Email) != null)
            {
                return "Found";
            }
            else
            {
                return "Not Found";
            }
        }

        public List<UserViewModel> GetUsers()
        {
            return this.us.GetUsers();
        }
    }
}
