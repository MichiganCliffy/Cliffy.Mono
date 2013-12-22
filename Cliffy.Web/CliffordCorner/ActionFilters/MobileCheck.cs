using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Configuration;
using Cliffy.Web.CliffordCorner.Controllers;
using FlickrNet;

namespace Cliffy.Web.CliffordCorner.ActionFilters
{
    public class MobileCheck : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller as BaseController;
            if (controller != null)
            {
                if (filterContext.HttpContext.Request.UserAgent.IndexOf("iphone", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    controller.IsMobileRequest = true;
                }
            }
        }
    }
}