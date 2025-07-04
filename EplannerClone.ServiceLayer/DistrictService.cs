using System.Collections.Generic;
using AutoMapper;
using EplannerClone.DomainModels;
using EplannerClone.Repositories;
using EplannerClone.ViewModels;

namespace EplannerClone.ServiceLayer
{
    public interface IDistrictService
    {
        List<DistrictsViewModel> GetAllDistricts();
    }
    public class DistrictService : IDistrictService
    {
        IDistrictRepository dr;
        public DistrictService()
        {
            dr = new DistrictRepository();
        }
        public List<DistrictsViewModel> GetAllDistricts()
        {
            List<District> d = dr.GetDistricts();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<District, DistrictsViewModel>(); });
            IMapper mapper = config.CreateMapper();
            List<DistrictsViewModel> dvm = mapper.Map<List<District>, List<DistrictsViewModel>>(d);
            return dvm;
        }
    }
}
