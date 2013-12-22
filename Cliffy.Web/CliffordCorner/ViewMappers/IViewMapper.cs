using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cliffy.Web.CliffordCorner.ViewMappers
{
    public delegate void RedirectEventHandler(object sender, string redirectTo);

    public interface IViewMapper
    {
        event RedirectEventHandler Redirect;
        string MapView(IPage model);
    }
}