using ElectionPlanner.Models.DAL;
using ElectionPlanner.Models.DTO;
using ElectionPlanner.Models.DTO.Ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectionPlanner.Models.BAL
{
    
    public class PollDayBLL
    {
        private readonly PollDayDAL pollDayDAL = new PollDayDAL();

        public List<PollDayDTO> GetPollDay()
        {
            return pollDayDAL.GetPollDay();
        }

        public bool AddPollDay(PollDayDTO country)
        {
            return pollDayDAL.AddPollDay(country);
        }

        public bool UpdatePollDay(PollDayDTO country)
        {
            return pollDayDAL.UpdatePollDay(country);
        }

        public List<RemarksDTO> GetRemarks()
        {
            return pollDayDAL.GetRemarks();
        }
        public List<GetAllDistrictDTO> GetAllDistricts()
        {
            return pollDayDAL.GetAllDistricts();
        }

        public List<GetAllAcDTO> GetAcByDistrict(int districtId)
        {
            return pollDayDAL.GetAcByDistrict(districtId);
        }

        //public ActivitiesDTO GetActivityByAc(int districtId, int acId) // 1 time
        //{
        //    return pollDayDAL.GetActivityByAc(districtId, acId);
        //}
        public ActivitiesDTO GetActivityDetailsByAc(int district, int ac) // 2 time
        {
            return pollDayDAL.GetActivityDetailsByAc(district, ac);
        }
        public List<ActivitiesDTO> GetRemarksWithActivitiesJoin()
        {
            return pollDayDAL.GetRemarksWithActivitiesJoin();
        }
        
    }

    
}