using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DTO
{
    public class TestVM:TestDTO
    {
        public int EmployeeId { get; set; }
        // Existing properties...
        //public int DistrictId { get; set; }
        //public int BlockId { get; set; }
        //public int FacilityId { get; set; }

        public IEnumerable<SelectListItem> DistrictType { get; set; }
        public IEnumerable<SelectListItem> BlockType { get; set; }
        public IEnumerable<SelectListItem> FacilityType { get; set; }

        public List<TestVM> EmployeeList { get; set; }

    }
}
