using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Cliffy.Web.ContentEngine;

public partial class Controls_ContentEngineMenuItem : System.Web.UI.UserControl
{
    private IContentPage mThisTag = null;
    private string mSelectedTag = string.Empty;
    private string mBasePath = string.Empty;

    public IContentPage ThisTag
    {
        get { return this.mThisTag; }
        set { this.mThisTag = value; }
    }

    public string SelectedTag
    {
        get { return this.mSelectedTag; }
        set { this.mSelectedTag = value; }
    }

    public string BasePath
    {
        get { return this.mBasePath; }
        set { this.mBasePath = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Visible = true;
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.Visible = false;
        if (this.mThisTag != null)
        {
            this.Visible = true;

            this.lnkMenuItem.ToolTip = this.mThisTag.Title;
            this.lnkMenuItem.Text = this.mThisTag.Title;

            if (this.mThisTag is ContentPageFlickrTag)
            {
                ContentPageFlickrTag flickrTag = (ContentPageFlickrTag)this.mThisTag;
                if (string.Compare(this.mSelectedTag, flickrTag.Tag, true) != 0)
                {
                    this.lnkMenuItem.NavigateUrl = this.ResolveUrl(string.Format("{0}/photographs/{1}", this.mBasePath, flickrTag.Tag));
                }
            }

            if (this.mThisTag is ContentPageHtml)
            {
                if ((string.Compare(this.mSelectedTag, "notes", true) != 0) &&
                    (string.Compare(this.mSelectedTag, this.mThisTag.Title, true) != 0))
                {
                    string title = Server.UrlEncode(this.mThisTag.Title).ToLower();
                    this.lnkMenuItem.NavigateUrl = this.ResolveUrl(string.Format("{0}/notes/{1}", this.mBasePath, title));
                }
            }
        }
    }
}