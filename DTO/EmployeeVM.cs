using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace DTO
{
    public class EmployeeVM
    {
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Employee name cann't be empty area.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Select designation")]
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public IEnumerable<SelectListItem> DesignationType { get; set; }
        [Required(ErrorMessage = "Contact number cann't be empty area.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be exactly 10 digits.")]
        public string MobileNumber { get; set; }
        [Required(ErrorMessage = "Select district")]
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public IEnumerable<SelectListItem> DistrictType { get; set; }
        [Required(ErrorMessage = "Select block")]
        public int BlockId { get; set; }
        public string BlockName { get; set; }
        public IEnumerable<SelectListItem> BlockType { get; set; }
        [Required(ErrorMessage = "Select facility name.")]
        public int FacilityId { get; set; }
        public string FacilityName { get; set; }
        public IEnumerable<SelectListItem> FacilityType { get; set; }
        public DateTime AddDate { get; set; }
        public int UserId { get; set; }
        public bool Istrained { get; set; }
        public bool IsUpdate { get; set; } = false;
        // Single dropdown for day, month, and year
        public DateTime? TrainingCompletionYear { get; set; }

        // For the dropdown list of dates
        public IEnumerable<SelectListItem> TrainingCompletionDates { get; set; }
    }
}
