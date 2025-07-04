using EplannerClone.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EplannerClone.Repositories
{
    public interface IActivityRepository
    {
        void InsertActivity(Activity a);
        void DeleteActivity(int aid);
        void UpdateActivity(Activity a);
        int GetLatestActivityID();
    }
    public class ActivityRepository : IActivityRepository
    {
        EplannerDbDbContext db;
        public ActivityRepository() { db = new EplannerDbDbContext(); }
        public void DeleteActivity(int aid)
        {
            Activity a = db.Activities.Where(temp => temp.Id == aid).FirstOrDefault();
            if (a != null) { db.Activities.Remove(a); db.SaveChanges(); }
        }

        public int GetLatestActivityID()
        {
            int aid = db.Activities.Select(temp => temp.Id).Max();
            return aid;
        }

        public void InsertActivity(Activity a)
        {
            db.Activities.Add(a);
            db.SaveChanges();
        }

        public void UpdateActivity(Activity a)
        {
            Activity act=db.Activities.Where(temp=>temp.Id==a.Id).FirstOrDefault();
            if(act != null)
            {
                act.Remark = a.Remark;
                act.District = a.District;
                act.Ac = a.Ac;
                act.PollDaySubLayer= a.PollDaySubLayer;
                act.UserId = a.UserId;
            }
        }
    }
}
