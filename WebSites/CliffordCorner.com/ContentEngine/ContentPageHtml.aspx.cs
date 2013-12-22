using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.XPath;
using Cliffy.Web.ContentEngine;

/// <summary>
/// Notes on the Trip.
/// </summary>
public partial class ContentEngine_ContentPageHtml : BasePageFlickrSet
{
    private string mContentTitle = string.Empty;

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (Request.QueryString["h"] != null)
        {
            if (Request.QueryString["h"].Length > 0)
            {
                this.mContentTitle = Request.QueryString["h"];
            }
        }

        if (this.mContent == null)
        {
            this.mErrorFlickr.Visible = true;
            return;
        }

        try
        {
            if (this.mContent != null)
            {
                this.AddMetaKeyword(this.mContent.Name);

                this.LoadMetaData();
                this.LoadMenu();

                this.mTitle.Visible = false;
                this.pnlContent.Visible = false;
                if (this.mContent.Pages.Count > 0)
                {
                    ContentPageHtml page = this.mContent.Pages.GetHtml(this.mContentTitle);

                    if (page.Title.Length > 0)
                    {
                        this.mTitle.Visible = true;
                        this.mTitle.InnerText = page.Title;
                    }

                    if (page.Path.Length > 0)
                    {
                        string filepath = Server.MapPath(this.ResolveUrl(page.Path));
                        if (File.Exists(filepath))
                        {
                            Literal literal = new Literal();
                            using (StreamReader content = File.OpenText(filepath))
                            {
                                literal.Text = content.ReadToEnd();
                                content.Close();
                            }

                            if (literal.Text.Length > 0)
                            {
                                this.pnlContent.Visible = true;
                                this.pnlContent.Controls.Add(literal);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            this.mErrorFlickr.Visible = true;
            this.mErrorFlickr.DataSource = ex;
        }
    }

    protected void Page_PreRender(object sender, System.EventArgs e)
    {
        if (this.mFlickrSetProvider != null)
        {
            this.mTitle.InnerText = this.mFlickrSetProvider.Title;
        }
    }

    private void LoadMetaData()
    {
        if (this.mContent != null)
        {
            if (this.mContent.Description.Length > 0)
            {
                this.Page.Title = string.Format("The Clifford Corner => {0}", this.mContent.Description);
                this.mMetaDescription = this.mContent.Description;
            }
            else
            {
                this.Page.Title = string.Format("The Clifford Corner => {0}", this.mContent.Name);
                this.mMetaDescription = this.mContent.Name;
            }

            this.AddMetaKeyword(this.mContent.Name);
            foreach (IContentPage tag in this.mContent.Pages)
            {
                if (tag is ContentPageFlickrTag) this.AddMetaKeyword(tag.Title);
            }
        }
    }

    private void LoadMenu()
    {
        if (this.mContent != null)
        {
            this.mMenu.Chapter = this.mContent;
            this.mMenu.DataBind();
        }
    }
}