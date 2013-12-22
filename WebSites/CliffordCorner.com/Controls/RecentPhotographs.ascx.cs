using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
///	Recently uploaded images at Flickr.
/// </summary>
public partial class Controls_RecentPhotographs : System.Web.UI.UserControl
{
    protected FlickrGroup mFlickr = new FlickrGroup();
    private List<FlickrImage> mPosts = new List<FlickrImage>();
    private int mCount = 10;

    public int Count
    {
        get { return this.mCount; }
        set { this.mCount = value; }
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        this.Visible = true;
        if (string.Compare(this.Page.Theme, "andreas04", true) == 0)
        {
            this.mCount = 12;
        }
    }

    protected void Page_PreRender(object sender, System.EventArgs e)
    {
        this.Visible = false;

        try
        {
            this.mFlickr.ImagesToShow = this.mCount;
            this.mFlickr.Load(ConfigurationManager.AppSettings["groupId"]);

            int counter = 0;
            foreach (FlickrImage image in this.mFlickr.Images)
            {
                this.mPosts.Add(image);

                counter++;
                if (counter >= this.mCount)
                    break;
            }

            if (this.mPosts.Count > 0)
            {
                this.Visible = true;
                this.mLinks.DataSource = this.mPosts;
                this.mLinks.DataBind();
            }
        }
        catch (SiteException ex)
        {
            Debug.WriteLine(string.Format("{0}/n{1}", ex.Message, ex.StackTrace));
        }
        catch (Exception ex)
        {
            Debug.WriteLine(string.Format("{0}/n{1}", ex.Message, ex.StackTrace));
        }
    }

    protected void Links_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        switch (e.Item.ItemType)
        {
            case ListItemType.Header:
                break;

            case ListItemType.Item:
            case ListItemType.AlternatingItem:
                FlickrImage image = (FlickrImage)e.Item.DataItem;

                HtmlAnchor link = (HtmlAnchor)e.Item.FindControl("mLink");
                link.HRef = string.Format(link.HRef, image.Identifier, image.SecretID);
                link.InnerText = image.Title;
                break;

            case ListItemType.Footer:
                break;
        }
    }
}