using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class EmployeeInfo_EmployeeTrainingVM
    {
        public int Emp_Id { get; set; }
        public int Tra_Id { get; set; }
        public string District { get; set; }
        public string Block { get; set; }
        public string Facility { get; set; }
        public string Employee_Designation { get; set; }
        public string EmployeeName { get; set; }
        public string Mobile { get; set; }
        public bool Employee_Trained { get; set; }
        public string Training_Completion_Year { get; set; }
        public string Training_MonthYearName { get; set; }
        public string Batch { get; set; }
        public string Traning_Designation { get; set; }
        public string EmployeeNameOfTraining { get; set; }
        public DateTime From_Date { get; set; }
        public DateTime To_Date { get; set; }
    }
}
