using EplannerClone.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EplannerClone.Repositories
{
    public interface IAcRepository
    {
        List<Ac> GetAllAc();
    }
    public class AcRepository:IAcRepository
    {
        EplannerDbDbContext db;
        public AcRepository()
        {
            db = new EplannerDbDbContext();
        }
        public List<Ac> GetAllAc() {
            List<Ac> aclist = db.AcTable.ToList();
            return aclist;
        }
    }
}
