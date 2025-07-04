using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EplannerClone.ViewModels
{
    public class UserViewModel
    {
        public int UserID { get; set; }
        [Required(ErrorMessage ="Email is required.")]
        [RegularExpression(@"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})",ErrorMessage ="Please enter valid e-mail address.")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage ="Confirmed password is required.")]
        [System.ComponentModel.DataAnnotations.Compare("Password",ErrorMessage ="Password didn't match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage ="Username is required.")]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string Username { get; set; }

        [Required(ErrorMessage ="Mobile number is required.")]
        public string ContactNo { get; set; }

        [ForeignKey("RoleId")]
        public int RoleId { get; set; } // Store the selected Role
       // public string RoleName { get; set; } // Store the selected Role
        public List<SelectListItem> Roles { get; set; } // Dropdown for roles

    }
}
