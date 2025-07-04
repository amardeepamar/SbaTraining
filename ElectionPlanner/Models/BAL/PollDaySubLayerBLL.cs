using ElectionPlanner.Models.DAL;
using ElectionPlanner.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectionPlanner.Models.BAL
{
    public class PollDaySubLayerBLL
    {
        private readonly PollDaySubLayerDAL pdslDAL = new PollDaySubLayerDAL();

        public List<PollDaySubLayerDTO> GetPollDaySubLayer()
        {
            return pdslDAL.GetPollDaySubLayer();
        }

        public bool AddPollDaySubLayer(PollDaySubLayerDTO SubLayer)
        {
            return pdslDAL.AddPollDaySubLayer(SubLayer);
        }

        public bool UpdatePollDaySubLayer(PollDaySubLayerDTO SubLayer)
        {
            return pdslDAL.UpdatePollDaySubLayer(SubLayer);
        }

        public List<ActivitiesDTO> GetActivities()
        {
            return pdslDAL.GetActivities();
        }


        public bool SaveActivity(ActivitiesDTO activity)
        {
            return pdslDAL.SaveActivity(activity);
        }


    }
}