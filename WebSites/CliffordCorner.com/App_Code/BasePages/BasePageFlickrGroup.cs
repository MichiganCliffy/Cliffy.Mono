using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Base page for all pages that use a flickr group
/// </summary>
public abstract class BasePageFlickrGroup : BasePage
{
    protected FlickrGroup mFlickrGroupProvider = null;
    protected string mTags = string.Empty;
    protected int mPage = 0;

    protected override void OnLoad(EventArgs e)
    {
        this.LoadProvider();

        base.OnLoad(e);
    }

    protected override void OnUnload(EventArgs e)
    {
        base.OnUnload(e);
    }

    protected virtual void LoadProvider()
    {
        this.mPage = this.CurrentPage();
        this.mTags = this.CurrentTags();

        try
        {
            this.mFlickrGroupProvider = new FlickrGroup();
            this.mFlickrGroupProvider.CurrentPage = this.mPage;
            this.mFlickrGroupProvider.Load(ConfigurationManager.AppSettings["groupId"], this.mTags);
        }
        catch (SiteException x)
        {
            this.LoadException(x);
        }
        catch (Exception x)
        {
            this.LoadException(x);
        }
    }

    protected abstract void LoadException(SiteException x);
    protected abstract void LoadException(Exception x);

    protected virtual int CurrentPage()
    {
        if (Request.QueryString["page"] != null)
        {
            if (Request.QueryString["page"].Length > 0)
            {
                int page = 0;
                if (int.TryParse(Request.QueryString["page"], out page))
                {
                    return page;
                }
            }
        }

        return 0;
    }

    protected virtual string CurrentTags()
    {
        if (Request.Form["t"] != null)
        {
            if (Request.Form["t"].Length > 0)
            {
                return Request.Form["t"];
            }
        }

        if (Request.QueryString["t"] != null)
        {
            return Request.QueryString["t"];
        }

        return string.Empty;
    }
}