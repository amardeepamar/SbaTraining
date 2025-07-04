using EplannerClone.ServiceLayer;
using EplannerClone.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EplannerClone.Controllers
{
    public class ActivityController : Controller
    {
        IDistrictService _ds;
        IAcService _acs;
        IRemarkService _rs;
        IUsersService _us;
        IPollDayService _ps;
        IPollDaySubLayerService _pdsls;
        public ActivityController(IDistrictService ds,IAcService acs,IRemarkService rs,IUsersService us,IPollDayService ps,IPollDaySubLayerService pdsls)
        {
            _ds = ds;
            _acs = acs;
            _rs = rs;
            _us = us;
            _ps = ps;
            _pdsls = pdsls;
        }

        // GET: Activity/Add_Activity
        public ActionResult AddActivity()
        {
            ActivityViewModel avm = new ActivityViewModel();
            List<DistrictsViewModel> dvm = _ds.GetAllDistricts();
            ViewBag.dvm = dvm;
            List<AcViewModel> acVm = _acs.GetAllAc();
            ViewBag.acvm = acVm;
            List<RemarkViewModel> rvm = _rs.GetAllRemarkList();
            ViewBag.rvm = rvm;
            List<UserViewModel> uvm = _us.GetUsers();
            ViewBag.uvm = uvm;
            List<PollDayViewModel> pdvm = _ps.GetAllPollDayList();
            ViewBag.pdvm = pdvm;
            List<PollDaySubLayerViewModel> pdslvm = _pdsls.GetAllPollDaySublayerList();
            ViewBag.pdslvm= pdslvm;
            return View(avm);
        } 
    }
}