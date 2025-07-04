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
    public interface IAcService
    {
        List<AcViewModel> GetAllAc();
    }
    public class AcService : IAcService
    {
        IAcRepository ar;
        public AcService()
        {
            ar = new AcRepository();
        }
        public List<AcViewModel> GetAllAc()
        {
            List<Ac> a = ar.GetAllAc();
            var config=new MapperConfiguration(cfg => { cfg.CreateMap<Ac,AcViewModel>(); });
            IMapper mapper=config.CreateMapper();
            List<AcViewModel> avm = mapper.Map<List<Ac>, List<AcViewModel>>(a);
            return avm;
        }

    }
}
