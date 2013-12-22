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

/// <summary>
/// Default photograph page.
/// </summary>
public partial class Photograph : BasePageFlickrGroup
{
    private FlickrImage mFlickrImage = null;

    protected void Page_Load(object sender, System.EventArgs e)
    {
        this.mErrorFlickr.Visible = false;
        try
        {
            this.mFlickrImage = new FlickrImage();
            this.mFlickrImage.Load(
                Request.QueryString.Get("i"),
                Request.QueryString.Get("s"));

            this.mImageTags.DataSource = this.mFlickrImage.Tags;

            if (this.mFlickrGroupProvider != null)
            {
                this.mPopularTags.DataSource = this.mFlickrGroupProvider.Tags;
            }
        }
        catch (Exception x)
        {
            this.mErrorFlickr.Visible = true;
            this.mErrorFlickr.DataSource = x;
            this.mErrorFlickr.DataBind();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.mDescription.Visible = false;

        if ((!this.IsPostBack) || (!this.mErrorFlickr.Visible))
        {
            this.Page.Title = string.Format("The Clifford Corner => Photography => {0}", this.mFlickrImage.Title);
            this.mTitle.InnerText = this.mFlickrImage.Title;

            this.mDescription.InnerHtml = this.mFlickrImage.Description;
            if (!string.IsNullOrWhiteSpace(this.mFlickrImage.Description))
            {
                this.mDescription.Visible = true;
                this.mMetaDescription = Server.HtmlEncode(this.mFlickrImage.Description);
            }

            this.AddMetaKeyword("william clifford");
            foreach (string tag in this.mFlickrImage.Tags)
            {
                this.AddMetaKeyword(tag);
            }

            this.mImage.Src = this.mFlickrImage.Urls["Medium"];
            this.mImage.Alt = this.mFlickrImage.Title;

            if (this.mFlickrImage.DateTaken.CompareTo(DateTime.MinValue) > 0)
            {
                this.mDateTaken.InnerText = string.Format(this.mDateTaken.InnerText, this.mFlickrImage.DateTaken.ToShortDateString());
            }
            else
            {
                this.mDateTaken.Visible = false;
            }

            if (this.mFlickrImage.DateAdded.CompareTo(DateTime.MinValue) > 0)
            {
                this.mDateUploaded.InnerText = string.Format(this.mDateUploaded.InnerText, this.mFlickrImage.DateAdded.ToShortDateString());
            }
            else
            {
                this.mDateUploaded.Visible = false;
            }

            if (!string.IsNullOrWhiteSpace(this.mFlickrImage.Owner))
            {
                this.mOwnerName.InnerText = string.Format(this.mOwnerName.InnerText, this.mFlickrImage.Owner); ;
            }
            else
            {
                this.mOwnerName.Visible = false;
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
    }

    protected override void LoadException(SiteException x)
    {
        this.mErrorFlickr.DataSource = x;
        this.mErrorFlickr.Visible = true;
        this.mTitle.Visible = false;
        this.mImage.Visible = false;
        this.mDescription.Visible = false;
        this.mDateTaken.Visible = false;
        this.mDateUploaded.Visible = false;
        this.mOwnerName.Visible = false;
    }

    protected override void LoadException(Exception x)
    {
        this.mErrorFlickr.DataSource = new SiteException(SiteExceptionLocation.SiteCode, x.Message, x);
        this.mErrorFlickr.Visible = true;
        this.mTitle.Visible = false;
        this.mImage.Visible = false;
        this.mDescription.Visible = false;
        this.mDateTaken.Visible = false;
        this.mDateUploaded.Visible = false;
        this.mOwnerName.Visible = false;
    }
}