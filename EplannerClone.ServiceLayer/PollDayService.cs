using AutoMapper;
using EplannerClone.DomainModels;
using EplannerClone.Repositories;
using EplannerClone.ViewModels;
using System.Collections.Generic;

namespace EplannerClone.ServiceLayer
{
    public interface IPollDayService
    {
        List<PollDayViewModel> GetAllPollDayList();
    }
    public class PollDayService : IPollDayService
    {
        IPollDayRepository pdr;
        public PollDayService() 
        {
            pdr = new PollDayRepository();
        }
        public List<PollDayViewModel> GetAllPollDayList()
        {
            List<PollDay> pd = pdr.GetPollDayList();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<PollDay , PollDayViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<PollDayViewModel> pdvm = mapper.Map<List<PollDay>, List<PollDayViewModel>>(pd);
            return pdvm;
        }
    }
}
