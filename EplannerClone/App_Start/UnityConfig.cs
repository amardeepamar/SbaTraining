using EplannerClone.ServiceLayer;
using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.WebApi;

namespace EplannerClone
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<IUsersService, UsersService>();
            container.RegisterType<IRolesServices,RolesService>();
            container.RegisterType<IAcService, AcService>();
            container.RegisterType<IDistrictService, DistrictService>();
            container.RegisterType<IPollDayService,PollDayService>();
            container.RegisterType<IPollDaySubLayerService,PollDaySubLayerService>();
            container.RegisterType<IRemarkService,RemarkService>();
            container.RegisterType<IActivityService,ActivityService>();

            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}