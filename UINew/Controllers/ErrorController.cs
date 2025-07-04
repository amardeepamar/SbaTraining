using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UINew.Controllers
{
    public class ErrorController : Controller
    {
        // 404 - Not Found
        public ActionResult NotFound()
        {
            ViewBag.ErrorCode = 404;
            ViewBag.ErrorMessage = "Sorry, the page you requested could not be found.";
            ViewBag.RequestedUrl = Request.Url?.ToString();    
            return View("NotFound"); // A custom 404 error page view
        }

        // 500 - Internal Server Error
        public ActionResult ServerError()
        {
            return View();
        }

        public ActionResult GeneralError()
        {
            Exception exception = Server.GetLastError();
            LogError(exception);
            Response.Clear();
            Server.ClearError();

            return View("GeneralError");
        }
        public ActionResult UnauthorizedError()
        {
            TempData["UnauthorizedMessage"] = "You do not have permission to access this resource.";
            return View();
        }
        private void LogError(Exception exception)
        {
            ErrorLogService.LogError(exception);
        }
    }
}