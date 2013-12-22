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
/// Displays lists of Photographs from a travellop.
/// </summary>
public partial class ContentEngine_ContentPageFlickrTag : BasePageFlickrSet
{
    protected string mTag = string.Empty;

    protected override void LoadProvider()
    {
        if (Request.QueryString.Get("t") != null)
            this.mTag = Request.QueryString.Get("t").ToLower();

        if (this.mContent != null)
        {
            if (this.mContent.FlickrSetId != null)
            {
                if (this.mContent.FlickrSetId.Length > 0)
                {
                    this.mFlickrSetProvider = new FlickrSet();
                    this.mFlickrSetProvider.Load(this.mContent.FlickrSetId, this.mTag);
                }
            }
        }
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        this.pnlMessage.Visible = true;
        if ((this.mTag.Length <= 0) || (this.mContent == null))
        {
            this.mPhotographs.Visible = false;
            this.mErrorFlickr.Visible = true;
            return;
        }
    }

    protected void Page_PreRender(object sender, System.EventArgs e)
    {
        this.pnlMessage.Visible = false;
        if (this.mFlickrSetProvider != null)
        {
            if (this.mContent != null)
            {
                this.LoadMenu();

                this.mPhotographs.DataSource = this.mFlickrSetProvider.Images;
                this.mPhotographs.TravelLog = this.mContent.Name;
                this.mPhotographs.DataBind();

                foreach (FlickrImage image in this.mFlickrSetProvider.Images)
                {
                    if (!this.mSlideShowImages.ContainsKey(image.Urls["Medium"]))
                    {
                        this.mSlideShowImages.Add(image.Urls["Medium"], image.Title.Replace("'", string.Empty));
                    }
                }

                ContentPageFlickrTag tag = this.mContent.Pages.GetTag(this.mTag);
                if (tag != null)
                {
                    if (tag.Message.Length > 0)
                    {
                        this.pnlMessage.Visible = true;
                        this.txtMessage.Text = tag.Message;
                    }

                    this.mBreadCrumb.InnerText = tag.Title;
                    this.LoadMetaDescription(tag);
                }

                this.AddMetaKeyword(this.mFlickrSetProvider.Title);
            }
        }

        if (this.mTag.Length > 0)
        {
            this.AddMetaKeyword(this.mTag);
        }
    }

    private void LoadMetaDescription(ContentPageFlickrTag tag)
    {
        if (this.mContent != null)
        {
            string description = this.mContent.Name;
            if (this.mContent.Description.Length > 0)
            {
                description = this.mContent.Description;
            }

            this.mMetaDescription = string.Format("{0}. {1} photographs.", description, tag.Title);
            this.Page.Title = string.Format("The Clifford Corner => {0} => {1} Photographs", description, tag.Title);
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