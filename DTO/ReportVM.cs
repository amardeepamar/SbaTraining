using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DTO
{
    public class ReportVM
    {
        public int SN { get; set; }
        public string DistrictNameEn { get; set; }
        public string TotalTrainedStaffNurse { get; set; }
        public string TotalUnTrainedStaffNurse { get; set; }
        public string TotalTrainedANM { get; set; }
        public string TotalUnTrainedANM { get; set; }

    }

   
}
