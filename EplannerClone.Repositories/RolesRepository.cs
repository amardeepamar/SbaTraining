using EplannerClone.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EplannerClone.Repositories
{
    public interface IRolesRepository
    {
        List<Role> GetRoles();
    }
    public class RolesRepository:IRolesRepository
    {
        EplannerDbDbContext db;
        public RolesRepository()
        {
            db=new EplannerDbDbContext();
        }

        public List<Role> GetRoles()
        {
            List<Role> r = db.Roles.ToList();
            return r;
        }
    }
}
