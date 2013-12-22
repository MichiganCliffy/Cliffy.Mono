using System;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Cliffy.Web.ContentEngine;

/// <summary>
/// Common methods and properties for all flickr set pages.
/// </summary>
public partial class BasePageFlickrSet : BasePage
{
    protected ContentChapter mContent = null;
    protected FlickrSet mFlickrSetProvider = null;
    protected Dictionary<string, string> mSlideShowImages = new Dictionary<string, string>();

    protected override void OnLoad(EventArgs e)
    {
        if (this.Request.QueryString["l"] != null)
        {
            string name = this.Request.QueryString.Get("l");
            if (name.Length > 0)
            {
                ContentBook logs = (ContentBook)ConfigurationManager.GetSection("contentBook");
                this.mContent = logs.Find(name);
                this.LoadProvider();
            }
        }

        base.OnLoad(e);
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (this.Master is BaseMasterPage)
        {
            BaseMasterPage master = (BaseMasterPage)this.Master;
            master.SlideShowImages = this.mSlideShowImages;
        }
    }

    protected virtual void LoadProvider()
    {
        if (this.mContent != null)
        {
            if (this.mContent.FlickrSetId != null)
            {
                if (this.mContent.FlickrSetId.Length > 0)
                {
                    this.mFlickrSetProvider = new FlickrSet();
                    this.mFlickrSetProvider.Load(this.mContent.FlickrSetId);
                }
            }
        }
    }
}