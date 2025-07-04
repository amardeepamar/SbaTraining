using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ElectionPlanner.Models.DTO
{
    public class LoginMasterDTO
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please fill the Username Area")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please fill the Password Area")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please fill the email Area")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
        public string Imagepath { get; set; }
        [RegularExpression(@"^[a-zA-Z ]*$",ErrorMessage ="Only accept capital/small letters.")]
        [Required(ErrorMessage = "Please fill the Full name Area")]
        public string SurName { get; set; }
        [Required(ErrorMessage = "Please fill the mobile number Area")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be exactly 10 digits.")]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "Passwords do not match.")]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please select a Role type")]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public IEnumerable<SelectListItem> RoleType { get; set; }


        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public IEnumerable<SelectListItem> DistrictType { get; set; }
        public int AcId { get; set; }
        public string AcName { get; set; }
        public IEnumerable<SelectListItem> AcType { get; set; }


        [Display(Name = "User Image")]
        public HttpPostedFileBase UserImage { get; set; }
        public bool RememberMe { get; set; }
        public bool IsUpdate { get; set; } = false;
    }
}
