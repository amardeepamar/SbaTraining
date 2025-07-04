using AutoMapper;
using EplannerClone.DomainModels;
using EplannerClone.Repositories;
using EplannerClone.ViewModels;
using System.Collections.Generic;

namespace EplannerClone.ServiceLayer
{
    public interface IRemarkService
    {
        List<RemarkViewModel> GetAllRemarkList();
    }
    public class RemarkService : IRemarkService
    {
        IRemarkRepository rp;
        public RemarkService()
        {
            rp= new RemarkRepository();
        }
        public List<RemarkViewModel> GetAllRemarkList()
        {
            List<Remark> d = rp.GetRemarks();
            var con = new MapperConfiguration(cf => { cf.CreateMap<Remark, RemarkViewModel>(); cf.IgnoreUnmapped(); });
            IMapper mapper = con.CreateMapper();
            List<RemarkViewModel> rvm = mapper.Map<List<Remark>, List<RemarkViewModel>>(d);
            return rvm;
        }
    }
}
