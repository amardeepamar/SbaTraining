using EplannerClone.DomainModels;
using System.Collections.Generic;
using System.Linq;

namespace EplannerClone.Repositories
{
    public interface IPollDayRepository
    {
        List<PollDay> GetPollDayList();
    }
    public class PollDayRepository : IPollDayRepository
    {
        EplannerDbDbContext db;
        public PollDayRepository()
        {
            db=new EplannerDbDbContext();
        }
        public List<PollDay> GetPollDayList()
        {
            List<PollDay> pd = db.PollDays.ToList();
            return pd;
        }
    }
}
