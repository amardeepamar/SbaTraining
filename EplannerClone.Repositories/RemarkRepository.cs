using EplannerClone.DomainModels;
using System.Collections.Generic;
using System.Linq;

namespace EplannerClone.Repositories
{
    public interface IRemarkRepository
    {
        List<Remark> GetRemarks();
    }
    public class RemarkRepository:IRemarkRepository
    {
        EplannerDbDbContext db;
        public RemarkRepository()
        {
            db = new EplannerDbDbContext();
        }

        public List<Remark> GetRemarks()
        {
            List<Remark> r = db.Remarks.ToList();
            return r;
        }
    }
}
