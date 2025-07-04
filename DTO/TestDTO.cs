using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TestDTO
    {
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public int DesignationId { get; set; }
        public string DesignationName { get; set; } // Optional: if you join for display
        public int DistrictId { get; set; }
        public int BlockId { get; set; }
        public int FacilityId { get; set; }
        public bool Istrained { get; set; }
        public int? TrainingCompletionYear { get; set; }
    }
}
