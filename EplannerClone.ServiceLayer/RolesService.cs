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
    public interface IRolesServices
    {
        List<RoleViewModel> GetRoles();
    }
    public class RolesService:IRolesServices
    {
        IRolesRepository rs;
        public RolesService()
        {
            rs=new RolesRepository();
        }

        public List<RoleViewModel> GetRoles()
        {
            List<Role> r=rs.GetRoles();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Role, RoleViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<RoleViewModel> rvm=mapper.Map<List<Role>,List<RoleViewModel>>(r);
            return rvm;
        }
    }
}
