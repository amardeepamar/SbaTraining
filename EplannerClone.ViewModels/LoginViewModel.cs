using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EplannerClone.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Username is required.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please select a role.")]
        public int RoleId { get; set; } // Store the selected Role
        public List<RoleViewModel> Role { get; set; }
        
    }
}
