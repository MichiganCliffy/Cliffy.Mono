using System;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public abstract class BasePagePhotographs : BasePageFlickrGroup
{
    protected Dictionary<string, string> mSlideShowImages = new Dictionary<string, string>();

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (this.Master is BaseMasterPage)
        {
            BaseMasterPage master = (BaseMasterPage)this.Master;
            master.SlideShowImages = this.mSlideShowImages;
        }
    }
}