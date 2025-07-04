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
    public interface IPollDaySubLayerService
    {
        List<PollDaySubLayerViewModel> GetAllPollDaySublayerList();
    }
    public class PollDaySubLayerService : IPollDaySubLayerService
    {
        IPollDaySubLayerRepository pdslr;
        public PollDaySubLayerService()
        {
            pdslr = new PollDaySubLayerRepository();
        }
        public List<PollDaySubLayerViewModel> GetAllPollDaySublayerList()
        {
            List<PollDaySubLayer> pdsl = pdslr.GetAllSublayerList();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<PollDaySubLayer, PollDaySubLayerViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<PollDaySubLayerViewModel> pdslvm = mapper.Map<List<PollDaySubLayer>, List<PollDaySubLayerViewModel>>(pdsl);
            return pdslvm;
        }
    }
}
