using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SessionDTO
    {
        public  int UserID { get; set; }
        public  string Username { get; set; }
        public  string Namesurname { get; set; }
        public  string Imagepath { get; set; }
        public  string Email { get; set; }
        public  string Mobile { get; set; }
        public  bool IsTrained { get; set; }
        public  string Role { get; set; }
        public string RoleName { get; set; }
        public  int DistId { get; set; }
        public  int BlockId { get; set; }
        public  int FacilityId { get; set; }
       // public  int DesignationId { get; set; }
    }
}
