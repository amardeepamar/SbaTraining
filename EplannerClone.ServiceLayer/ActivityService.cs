using AutoMapper;
using EplannerClone.DomainModels;
using EplannerClone.Repositories;
using EplannerClone.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EplannerClone.ServiceLayer
{
    public interface IActivityService
    {
        int InsertActivity(ActivityViewModel avm);
        void UpdateActivity(ActivityViewModel avm);
        void DeleteActivity(int aid);
    }
    public class ActivityService : IActivityService
    {
        IActivityRepository ar;
        public ActivityService()
        {
            ar=new ActivityRepository();
        }

        public void DeleteActivity(int aid)
        {
            throw new NotImplementedException();
        }

        public int InsertActivity(ActivityViewModel avm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<ActivityViewModel, Activity>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Activity a = mapper.Map<ActivityViewModel, Activity>(avm);
            
            a.Remark = avm.Remark;
            a.Ac = avm.Ac;
            a.District=avm.District;
            a.PollDaySubLayer = avm.PollDaySubLayer;
            a.UserId = avm.UserId;

            ar.InsertActivity(a);
            int aid = ar.GetLatestActivityID();
            return aid;
        }

        public void UpdateActivity(ActivityViewModel avm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<ActivityViewModel, Activity>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Activity u = mapper.Map<ActivityViewModel, Activity>(avm);
            ar.UpdateActivity(u);
        }
    }
}
