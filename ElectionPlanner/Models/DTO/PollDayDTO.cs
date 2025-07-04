using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectionPlanner.Models.DTO
{
    public class PollDayDTO
    {
        public int Id { get; set; }
        public string PollDayNameEN { get; set; }
    }
    public class PollDaySubLayerDTO
    {
        public int Id { get; set; }
        public string PollDaySubLayerNameEN { get; set; }
        public int PollDayId { get; set; } // Foreign key from tblPollDayMaster
        public string PollDayNameEN { get; set; } // For display purposes
        public int? StartingDay { get; set; }
        public int? EndingDay { get; set; }
        public int? Duration { get; set; }
    }
    public class TentativeDTO
    {
        public int Id { get; set; }
        public string Activities { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Tentative_StateDate { get; set; }
        public DateTime Tentative_EndDate { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public int AcId { get; set; }
        public string StateName { get; set; }  // Change from StateId
        public string DistrictName { get; set; }  // Change from DistrictId
        public string AcName { get; set; }  // Change from AcId
        public bool IsProcessed { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime DeletedDate { get; set; }
        public DateTime LastUpdateDate { get; set; }

        
    }
}