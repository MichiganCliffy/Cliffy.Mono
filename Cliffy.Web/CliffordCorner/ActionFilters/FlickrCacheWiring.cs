using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Configuration;
using FlickrNet;

namespace Cliffy.Web.CliffordCorner.ActionFilters
{
    public class FlickrCacheWiring : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Flickr.CacheLocation = filterContext.HttpContext.Request.MapPath("~/App_Data");
            base.OnActionExecuting(filterContext);
        }
    }
}