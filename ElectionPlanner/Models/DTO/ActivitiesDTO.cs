using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectionPlanner.Models.DTO
{
    public class ActivitiesDTO
    {
        public int Id { get; set; }
        public string PollDayNameEn { get; set; }
        public string PollDaySubLayerNameEn { get; set; }
        public int StartingDay { get; set; }
        public int EndingDay { get; set; }
        public int Duration { get; set; }
        public DateTime TentativeStartingDate { get; set; }
        public DateTime TentativeEndingDate { get; set; }
        public int tblPollDaySubLayerMasterId { get; set; }
        public string Remark { get; set; }
        public int District { get; set; }
        public int Ac { get; set; }
        public int UserId { get; set; }
        public int? RemarkId { get; set; }


        public string RemarkName { get; set; }      // From Remark table
        public string DistrictNameEn { get; set; }  // from DistrictMaster
        public string AC_NAME_EN { get; set; }        // from tblAc
        public string Username { get; set; }          // from LoginMaster

       
    }

   



}