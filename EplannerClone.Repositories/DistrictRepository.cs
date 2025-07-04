using EplannerClone.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EplannerClone.Repositories
{
    public interface IDistrictRepository
    {
        List<District> GetDistricts();
    }
    public class DistrictRepository : IDistrictRepository
    {
        EplannerDbDbContext db;
        public DistrictRepository()
        {
            db = new EplannerDbDbContext();
        }
        public List<Role> GetRoles()
        {
            return db.Roles.ToList(); // Just return the Roles
        }
        public List<District> GetDistricts()
        {
            List<District> d = db.Districts.ToList();
            return d;
        }
    }
}
