using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.XPath;
using Cliffy.Web.ContentEngine;

public partial class Controls_ContentEngineMenu : System.Web.UI.UserControl
{
    private ContentChapter mChapter = null;
    private string mPage = string.Empty;
    private string mSelectedItem = string.Empty;

    public ContentChapter Chapter
    {
        get { return this.mChapter; }
        set { this.mChapter = value; }
    }

    protected string BasePath
    {
        get
        {
            if (this.mChapter != null)
            {
                string name = Server.UrlEncode(this.mChapter.Name).ToLower();
                if (!this.mChapter.IsTravelLog)
                {
                    return string.Format("~/{0}", name);
                }

                return string.Format("~/travellog/{0}", name);
            }

            return string.Empty;
        }
    }

    protected string SelectedItem
    {
        get { return this.mSelectedItem; }
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        this.mPage = Request.CurrentExecutionFilePath.ToLower();
        this.mPage = this.mPage.Substring(this.mPage.LastIndexOf("/") + 1);

        if (Request.QueryString.Get("t") != null)
        {
            this.mSelectedItem = Request.QueryString.Get("t");
        }

        if (string.Compare(this.mPage, "contentpagehtml.aspx", true) == 0)
        {
            this.mSelectedItem = "notes";
            if (Request.QueryString["h"] != null)
            {
                if (Request.QueryString["h"].Length > 0)
                {
                    this.mSelectedItem = Request.QueryString["h"];
                }
            }
        }
    }

    protected void Page_PreRender(object sender, System.EventArgs e)
    {
        if (this.mChapter != null)
        {
            if (string.Compare(this.mPage, "contentchapter.aspx", true) != 0)
            {
                string name = Server.UrlEncode(this.mChapter.Name).ToLower();

                if (!this.mChapter.IsTravelLog)
                {
                    this.lnkMenuHome.NavigateUrl = this.ResolveUrl(string.Format("~/{0}", name));
                }
                else
                {
                    this.lnkMenuHome.NavigateUrl = this.ResolveUrl(string.Format("~/travellog/{0}", name));
                }
            }

            this.mLinks.DataSource = this.mChapter.Pages;
            this.mLinks.DataBind();

            if (this.mChapter.Pages.MenuTitle.Length > 0)
            {
                this.txtMenuTitle.Text = this.mChapter.Pages.MenuTitle;
            }
        }
    }
}