using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Configuration;

namespace Cliffy.Web.CliffordCorner.ActionFilters
{
    public class AdminAuthCheck : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = filterContext.RequestContext.HttpContext.User;
            if ((user == null) || (!user.Identity.IsAuthenticated))
            {
                filterContext.Result = new RedirectResult(FormsAuthentication.LoginUrl);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}