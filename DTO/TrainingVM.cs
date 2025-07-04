using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DTO
{
    public class TrainingVM
    {
        public int Id { get; set; }
        public int MonthYearId { get; set; }
        public string MonthYearName { get; set; }
        public IEnumerable<SelectListItem> MonthYearType { get; set; }
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public IEnumerable<SelectListItem> DesignationType { get; set; }
        public int Employee_infoId { get; set; }
        public string EmployeeName { get; set; }
        public IEnumerable<SelectListItem> EmployeeType { get; set; }

        public int BatchId { get; set; }
        public string BatchName { get; set; }
        public IEnumerable<SelectListItem> BatchType { get; set; }
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }

        public DateTime AddDate { get; set; }
        public int UserId { get; set; }
        public bool IsUpdate { get; set; } = false;
        public List<LoginMasterDTO> LoginMasterList { get; set; }

        public string EmployeeDist { get; set; }
    }
}
