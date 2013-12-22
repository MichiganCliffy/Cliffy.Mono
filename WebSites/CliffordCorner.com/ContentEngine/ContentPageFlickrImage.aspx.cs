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

/// <summary>
/// Photograph page for a chapter.
/// </summary>
public partial class ContentEngine_ContentPageFlickrImage : BasePageFlickrSet
{
    private FlickrImage mFlickrImage = null;

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (this.mContent == null)
        {
            this.mErrorFlickr.Visible = true;
            return;
        }

        try
        {
            this.LoadMenu();

            this.mFlickrImage = new FlickrImage();
            this.mFlickrImage.Load(
                Request.QueryString.Get("i"),
                Request.QueryString.Get("s"));
        }
        catch (Exception ex)
        {
            this.mErrorFlickr.Visible = true;
            this.mErrorFlickr.DataSource = ex;
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.mBreadCrumb.InnerText = this.mFlickrImage.Title;
        this.mDescription.InnerHtml = this.mFlickrImage.Description;
        if (!string.IsNullOrWhiteSpace(this.mFlickrImage.Description))
        {
            this.mDescription.Visible = true;
        }
        else
        {
            this.mDescription.Visible = false;
        }

        this.mImage.Src = this.mFlickrImage.Urls["Medium"];
        this.mImage.Alt = this.mFlickrImage.Title;

        if (this.mFlickrSetProvider != null)
        {
            this.mTitle.InnerText = this.mFlickrSetProvider.Title;
        }

        if (this.mContent != null)
        {
            if (!string.IsNullOrWhiteSpace(this.mContent.Description))
            {
                this.Page.Title = string.Format("The Clifford Corner => {0} => {1}", this.mContent.Description, this.mFlickrImage.Title);
            }
            else
            {
                this.Page.Title = string.Format("The Clifford Corner => {0} => {1}", this.mContent.Name, this.mFlickrImage.Title);
            }
        }

        if (!string.IsNullOrWhiteSpace(this.mFlickrImage.Description))
        {
            this.mMetaDescription = Server.HtmlEncode(this.mFlickrImage.Description);
        }

        if (this.mContent != null) this.AddMetaKeyword(this.mContent.Name);
        foreach (string tag in this.mFlickrImage.Tags)
        {
            this.AddMetaKeyword(tag);
        }

        this.mImageLink.Title = string.Format(this.mImageLink.Title, this.mFlickrImage.Title);
        if (this.mFlickrImage.Urls["Original"] != null)
        {
            if (this.mFlickrImage.Urls["Original"].Length > 0)
            {
                this.mImageLink.HRef = this.mFlickrImage.Urls["Original"];
            }
            else
            {
                this.mImageLink.Disabled = true;
            }
        }
        else
        {
            this.mImageLink.Disabled = true;
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