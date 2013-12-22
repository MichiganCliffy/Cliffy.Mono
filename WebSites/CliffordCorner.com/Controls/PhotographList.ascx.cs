using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
///	Control for displaying a list of Flick Photographs.
/// </summary>
public partial class Controls_PhotographList : System.Web.UI.UserControl
{
    private FlickrImage[] mDataSource = null;
    private string mTag = string.Empty;
    private string mTravelLog = string.Empty;
    private NameValueCollection mQueryParameters = new NameValueCollection();
    private bool mAddBreak = false;

    public FlickrImage[] DataSource
    {
        get { return this.mDataSource; }
        set { this.mDataSource = value; }
    }

    public string Tag
    {
        get { return this.mTag; }
        set { this.mTag = value; }
    }

    public bool AddBreak
    {
        get { return this.mAddBreak; }
        set { this.mAddBreak = value; }
    }

    public string TravelLog
    {
        get { return this.mTravelLog; }
        set { this.mTravelLog = value; }
    }

    public NameValueCollection QueryParameters
    {
        get { return this.mQueryParameters; }
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
    }

    protected void Page_PreRender(object sender, System.EventArgs e)
    {
        if (this.mDataSource != null)
        {
            if (this.mDataSource.Length > 0)
            {
                this.mImages.DataSource = this.mDataSource;
                this.mImages.DataBind();
            }
            else
            {
                this.mImages.Visible = false;
                this.mNoImagesText.Visible = true;
            }
        }
        else
        {
            this.mImages.Visible = false;
            this.mNoImagesText.Visible = true;
        }
    }

    protected void Images_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        switch (e.Item.ItemType)
        {
            case ListItemType.Header:
                Literal breadcrumb = (Literal)e.Item.FindControl("mBreadCrumb");
                if (this.mTag.Length > 0)
                {
                    breadcrumb.Text = string.Format("Images tagged with \"{0}\"<br /><br />", this.mTag);
                }
                else
                {
                    if (!this.mAddBreak)
                    {
                        e.Item.Visible = false;
                    }
                    else
                    {
                        breadcrumb.Text = "&nbsp;<br /><br />";
                    }
                }
                break;

            case ListItemType.Item:
            case ListItemType.AlternatingItem:
                FlickrImage flickrImage = (FlickrImage)e.Item.DataItem;
                if (flickrImage != null)
                {

                    HtmlImage image = (HtmlImage)e.Item.FindControl("mImage");
                    image.Src = flickrImage.Urls["Thumbnail"];
                    image.Alt = flickrImage.Title;

                    HtmlAnchor link = (HtmlAnchor)e.Item.FindControl("mLink");

                    link.HRef = this.BuildUrl(link.HRef, flickrImage);
                    foreach (string name in this.mQueryParameters.AllKeys)
                        link.HRef = string.Format("{0}&{1}={2}", link.HRef, name, this.mQueryParameters[name]);
                    link.Title = flickrImage.Title;
                }
                break;

            case ListItemType.Footer:
                break;
        }
    }

    private string BuildUrl(string page, FlickrImage image)
    {
        StringBuilder url = new StringBuilder();
        url.Append("~/");

        if (this.mTravelLog.Length > 0)
            url.Append(string.Format("travellog/{0}/", this.mTravelLog));

        url.Append("photograph");
        url.Append(string.Format("/{0}", image.Identifier));
        url.Append(string.Format("/{0}", image.SecretID));

        //url.Insert(0, string.Format("{0}?", page));

        return this.ResolveUrl(url.ToString());
    }
}