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

public partial class Controls_NavTopExtensionItem : System.Web.UI.UserControl
{
    private ContentBook mAllLogs = (ContentBook)ConfigurationManager.GetSection("contentBook");
    private ContentChapter mChapter = null;

    public ContentChapter Chapter
    {
        get { return this.mChapter; }
        set { this.mChapter = value; }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.Visible = false;
        if (this.mChapter != null)
        {
            this.Visible = true;

            string name = Server.UrlEncode(this.mChapter.Name).ToLower();

            if (this.mChapter.RedirectTo.Length > 0)
            {
                string redirectTo = string.Empty;
                if (this.mChapter.RedirectTo.Contains("~"))
                {
                    redirectTo = this.ResolveUrl(this.mChapter.RedirectTo).ToLower();
                }
                else
                {
                    redirectTo = this.mChapter.RedirectTo.ToLower();
                }

                this.lnkNavTop.NavigateUrl = redirectTo;
            }
            else if (!this.mChapter.IsTravelLog)
            {
                this.lnkNavTop.NavigateUrl = this.ResolveUrl(string.Format("~/{0}", name));
            }
            else
            {
                this.lnkNavTop.NavigateUrl = this.ResolveUrl(string.Format("~/travellogs/{0}", name));
            }

            this.lnkNavTop.Text = this.mChapter.Name;

            if (this.mChapter.Description.Length > 0)
            {
                this.lnkNavTop.ToolTip = this.mChapter.Description;
                this.ctrMoreItem.Attributes["title"] = this.mChapter.Description;
            }

            ContentChapter lastOne = (ContentChapter)this.mAllLogs.Chapters.GetByIndex(this.mAllLogs.Chapters.Count - 1);
            if (this.mChapter.Position < lastOne.Position)
            {
                this.ctrMoreItem.Style["border-bottom"] = "solid 1px #ffffff";
            }
        }
    }
}