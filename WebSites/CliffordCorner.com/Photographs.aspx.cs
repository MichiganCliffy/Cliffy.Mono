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

/// <summary>
/// Displays lists of Photographs.
/// </summary>
public partial class Photographs : BasePagePhotographs
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (this.mFlickrGroupProvider != null)
        {
            this.mPopularTags.DataSource = this.mFlickrGroupProvider.Tags;
            if (this.mTags.Length > 0)
            {
                this.mPopularTags.Title = "Associated Tags";
                this.mPhotographs.Tag = this.mTags;
            }

            this.mPhotographs.DataSource = this.mFlickrGroupProvider.Images;

            this.mPaging.ItemPerPage = this.mFlickrGroupProvider.ImagesToShow;
            this.mPaging.ItemTotal = this.mFlickrGroupProvider.NumberOfImages;
            this.mPaging.PageCurrent = this.mFlickrGroupProvider.CurrentPage;
            if (this.mTags.Length > 0)
            {
                this.mPaging.PageToUse = string.Format("{0}/{1}", this.mPaging.PageToUse, this.mTags);
            }
            this.mPaging.DataBind();
        }
    }

    protected void Page_PreRender(object sender, System.EventArgs e)
    {
        this.Page.Title = "The Clifford Corner => Photographs by, about, and related to the Clifford Family.";
        this.mMetaDescription = "Photographs by, about, and from the Clifford Family of Ann Arbor, Michigan (and other parts of the world).";
        if (this.mTags.Length > 0)
        {
            string title = string.Format(" Photographs about {0}{1}.", this.mTags.Substring(0, 1).ToUpper(), this.mTags.Substring(1).ToLower());
            this.Page.Title += title;

            this.AddMetaKeyword(this.mTags);

            string description = string.Format(" All photographs tagged with {0}{1}", this.mTags.Substring(0, 1).ToUpper(), this.mTags.Substring(1).ToLower());
            this.mMetaDescription += description;
        }

        if (this.mFlickrGroupProvider != null)
        {
            foreach (FlickrImage image in this.mFlickrGroupProvider.Images)
            {
                if (!this.mSlideShowImages.ContainsKey(image.Urls["Medium"]))
                {
                    this.mSlideShowImages.Add(image.Urls["Medium"], image.Title.Replace("'", string.Empty));
                }
            }
        }
    }

    protected override void LoadException(Exception x)
    {
        this.mErrorFlickr.DataSource = new SiteException(SiteExceptionLocation.SiteCode, x.Message, x);
        this.mErrorFlickr.Visible = true;
    }

    protected override void LoadException(SiteException x)
    {
        this.mErrorFlickr.DataSource = x;
        this.mErrorFlickr.Visible = true;
    }
}