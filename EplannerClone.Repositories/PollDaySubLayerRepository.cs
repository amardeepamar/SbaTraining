using EplannerClone.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EplannerClone.Repositories
{
    public interface IPollDaySubLayerRepository
    {
        List<PollDaySubLayer> GetAllSublayerList(); 
    }
    public class PollDaySubLayerRepository : IPollDaySubLayerRepository
    {
        EplannerDbDbContext db;
        public PollDaySubLayerRepository()
        {
            db= new EplannerDbDbContext();
        }
        public List<PollDaySubLayer> GetAllSublayerList()
        {
            List<PollDaySubLayer> pdsl=db.PollDaySubLayers.ToList();
            return pdsl;
        }
    }
}
