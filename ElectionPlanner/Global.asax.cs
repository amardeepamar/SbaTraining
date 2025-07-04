using CaptchaMvc.Infrastructure;
using CaptchaMvc.Interface;
using CaptchaMvc.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ElectionPlanner
{
    public class MvcApplication : System.Web.HttpApplication
    {
       
        protected void Application_Start()
        {
           
            #region For Captcha
            var captchaManager = (DefaultCaptchaManager)CaptchaUtils.CaptchaManager;
            captchaManager.CharactersFactory = () => "abcdefghijklmnopqrstuvwxyz123456789";
            captchaManager.PlainCaptchaPairFactory = length =>
            {
                string randomText = RandomText.Generate(captchaManager.CharactersFactory(), length);
                bool ignoreCase = false;
                return new KeyValuePair<string, ICaptchaValue>(Guid.NewGuid().ToString(format: "N"),
                    new StringCaptchaValue(randomText, randomText, ignoreCase));
            };
            #endregion
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    var exception = Server.GetLastError();
        //    Response.Clear();
        //    Server.ClearError();

        //    // Log the exception (optional)
        //    // LogException(exception);

        //    // Redirect to custom error page
        //    Response.Redirect("~/Error/GeneralError");
        //}

        // FilterConfig.cs located in App_Start folder 

        //public class FilterConfig
        //{
        //    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        //    {
        //        filters.Add(new HandleErrorAttribute());
        //    }
        //}

    }
}
