using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using Cliffy.Web.ContentEngine;

/// <summary>
/// Landing Page for any chapter.
/// </summary>
public partial class ContentEngine_ContentChapter : BasePageFlickrSet
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (this.mContent != null)
            {
                this.AddMetaKeyword(this.mContent.Name);
                if (this.mContent.Pages != null)
                {
                    foreach (IContentPage tag in this.mContent.Pages)
                    {
                        if (tag is ContentPageFlickrTag) this.AddMetaKeyword(tag.Title);
                    }
                }

                if (this.mContent.Description.Length > 0)
                {
                    this.Page.Title = string.Format("The Clifford Corner => {0}", this.mContent.Description);
                    this.mMetaDescription = this.mContent.Description;
                }
                else
                {
                    this.Page.Title = string.Format("The Clifford Corner => {0}", this.mContent.Description);
                    this.mMetaDescription = this.mContent.Description;
                }
            }
        }
        catch (Exception ex)
        {
            this.pnlContent.Visible = false;
            this.mErrorFlickr.Visible = true;
            this.mErrorFlickr.DataSource = ex;
        }
    }

    protected void Page_PreRender(object sender, System.EventArgs e)
    {
        try
        {
            this.ctrMenu_PreRender(sender, e);
            this.txtChapterTitle_PreRender(sender, e);
            this.txtSummary_PreRender(sender, e);
            this.imgImage_PreRender(sender, e);
        }
        catch (Exception ex)
        {
            this.pnlContent.Visible = false;
            this.mErrorFlickr.Visible = true;
            this.mErrorFlickr.DataSource = ex;
        }
    }

    protected void txtSummary_PreRender(object sender, EventArgs e)
    {
        if (this.mFlickrSetProvider != null)
        {
            if (this.mFlickrSetProvider.Description.Length > 0)
            {
                this.txtSummary.Text = this.mFlickrSetProvider.Description;
                return;
            }
        }

        if (this.mContent != null)
        {
            this.txtSummary.Text = this.mContent.Description;
        }
    }

    protected void txtChapterTitle_PreRender(object sender, EventArgs e)
    {
        if (this.mFlickrSetProvider != null)
        {
            if (this.mFlickrSetProvider.Title.Length > 0)
            {
                this.txtChapterTitle.Text = this.mFlickrSetProvider.Title;
                return;
            }
        }

        if (this.mContent != null)
        {
            this.txtChapterTitle.Text = this.mContent.Name;
        }
    }

    protected void imgImage_PreRender(object sender, EventArgs e)
    {
        this.imgImage.Visible = false;
        if (this.mFlickrSetProvider != null)
        {
            if (this.mFlickrSetProvider.PhotoSetId.Length > 0)
            {
                if (this.mFlickrSetProvider.DefaultImage != null)
                {
                    this.imgImage.Visible = true;
                    this.imgImage.Src = this.mFlickrSetProvider.DefaultImage.Urls["small"];
                }
            }
        }
    }

    protected void ctrMenu_PreRender(object sender, EventArgs e)
    {
        if (this.mContent != null)
        {
            this.ctrMenu.Chapter = this.mContent;
            this.ctrMenu.DataBind();
        }
    }
}