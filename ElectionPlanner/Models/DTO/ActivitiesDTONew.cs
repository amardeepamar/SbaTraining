using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectionPlanner.Models.DTO
{
    public class ActivitiesDTONew
    {
        public int Id { get; set; }
        public string PollDayNameEn { get; set; }       
        public string PollDayId { get; set; }
        public int StartingDay { get; set; }
        public int EndingDay { get; set; }
        public int Duration { get; set; }
        public DateTime TentativeStartingDate { get; set; }
        public DateTime TentativeEndingDate { get; set; }
        public int PollDaySubLayerId { get; set; }
        public string PollDaySubLayerNameEn { get; set; } // from PollDaySubLayerMaster
        public int District { get; set; }
        public string DistrictNameEn { get; set; }  // from DistrictMaster
        public int Ac { get; set; }
        public string AC_NAME_EN { get; set; }        // from tblAc
        public int UserId { get; set; }
        public string Username { get; set; }          // from LoginMaster
        public int? RemarkId { get; set; }
        public string RemarkName { get; set; }      // From Remark table
        
       
       

       
    }

   



}