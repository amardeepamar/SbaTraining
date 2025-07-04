using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionPlanner.Models.DTO
{
    public class SessionDTO
    {
        public  int UserID { get; set; }
        public  string Username { get; set; }
        public  string Namesurname { get; set; }
        public  string Imagepath { get; set; }
        public  string Email { get; set; }
        public  string Mobile { get; set; }
        public  int Role { get; set; }
        public string RoleName { get; set; }
        public  int DistId { get; set; }
        public  int AcId { get; set; }
    }
}
