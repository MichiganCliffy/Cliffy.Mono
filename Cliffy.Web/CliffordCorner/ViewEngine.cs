using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Cliffy.Web.CliffordCorner
{
    public class ViewEngine : RazorViewEngine
    {
        public ViewEngine() : base()
        {
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            partialViewName = string.Concat("default/", partialViewName);
            return base.FindPartialView(controllerContext, partialViewName, useCache);
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            viewName = string.Concat("default/", viewName);
            return base.FindView(controllerContext, viewName, masterName, useCache);
        }
    }
}
