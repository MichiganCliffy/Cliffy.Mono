using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cliffy.Web.CliffordCorner.ViewMappers
{
    public class MobileViewMapper : IViewMapper
    {
        public event RedirectEventHandler Redirect;

        public string MapView(IPage model)
        {
            if ((model is PagePhotoSet) || (model is PagePhotoSetTag) || (model is PagePhotoSetPhoto))
            {
                    return "MobilePhotoSet";
            }
            else if (model is PageRedirect)
            {
                if (Redirect != null)
                {
                    PageRedirect redirect = (PageRedirect)model;
                    Redirect(this, redirect.RedirectTo);
                }
            }

            return "MobileIndex";
        }
    }
}