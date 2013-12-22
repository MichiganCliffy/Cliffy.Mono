using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.XPath;
using Cliffy.Web.ContentEngine;

/// <summary>
///	Controls the display for top level navigation elements.
/// </summary>
public partial class Controls_NavTop : System.Web.UI.UserControl
{
    protected string mPageName = string.Empty;
    protected string mPagePath = string.Empty;
    private string mHomePath = string.Empty;
    private int mLimit = 10;

    private ContentBook mAllLogs = (ContentBook)ConfigurationManager.GetSection("contentBook");
    private ContentBook mLogs = null;
    private ContentBook mLogsContinued = null;

    protected void Page_Load(object sender, System.EventArgs e)
    {
        this.mHomePath = string.Concat(this.ResolveUrl("~/"), "default.aspx");
        this.mPagePath = Request.CurrentExecutionFilePath.ToLower();
        this.mPageName = this.mPagePath.Substring(this.mPagePath.LastIndexOf("/") + 1);

        this.LoadLogs();
        Trace.Write("aspx.page", string.Format("There are {0} travellogs", this.mAllLogs.Chapters.Count));
    }

    protected void Page_PreRender(object sender, System.EventArgs e)
    {
        this.mHomeLink.HRef = this.ResolveUrl("~/");

        this.mPhotographsLink.HRef = this.ResolveUrl(this.mPhotographsLink.HRef);

        if ((string.Compare(this.mHomePath, Request.Path, true) == 0) ||
            (this.mPagePath.Contains("/blogposts/")))
        {
            this.mHome.Attributes["class"] = "selected";
        }

        if ((this.mPagePath.Contains("/photographs.aspx")) ||
            (this.mPagePath.Contains("/photograph.aspx")))
        {
            this.mPhotographs.Attributes["class"] = "selected";
        }

        this.rptTravelLogs_PreRender(sender, e);
        this.ctrExtension_PreRender(sender, e);
    }

    private void LoadLogs()
    {
        if (this.mAllLogs != null)
        {
            this.mLogs = new ContentBook();
            if (this.mAllLogs.Chapters.Count > this.mLimit)
            {
                this.mLogsContinued = new ContentBook();

                int i = 0;
                int k = 0;
                while (i < this.mLimit)
                {
                    if (this.mAllLogs.Chapters.ContainsKey(k))
                    {
                        this.mLogs.Chapters.Add(k, this.mAllLogs.Chapters[k]);
                        i++;
                    }
                    k++;
                }

                foreach (ContentChapter travellog in this.mAllLogs.Chapters.Values)
                {
                    if (this.mLogs.Chapters.ContainsKey(travellog.Position)) continue;
                    this.mLogsContinued.Chapters.Add(travellog.Position, travellog);
                }
            }
            else
            {
                foreach (ContentChapter travellog in this.mAllLogs.Chapters.Values)
                {
                    this.mLogs.Chapters.Add(travellog.Position, travellog);
                }
            }
        }
    }

    protected void rptTravelLogs_PreRender(object sender, EventArgs e)
    {
        this.rptTravelLogs.Visible = false;
        if (this.mLogs != null)
        {
            if (this.mLogs.Chapters.Count > 0)
            {
                this.rptTravelLogs.Visible = true;
                this.rptTravelLogs.DataSource = this.mLogs.Chapters.Values;
                this.rptTravelLogs.DataBind();
            }
        }
    }

    protected void ctrExtension_PreRender(object sender, EventArgs e)
    {
        this.ctrExtension.Book = this.mLogsContinued;
        this.ctrExtension.DataBind();
    }
}