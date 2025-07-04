using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EplannerClone.ViewModels
{
    public class EditUserPasswordViewModel
    {
        public int UserID { get; set; }

        [Required]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmed password is required.")]
        [Compare("Password",ErrorMessage ="Confirm password don't match with password.")]
        public string ConfirmPassword { get; set; }
    }
}
