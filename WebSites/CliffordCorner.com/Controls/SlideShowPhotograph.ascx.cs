using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
///	Control that JUST displays html image tags for a Flickr Image.
/// </summary>
public partial class Controls_SlideShowPhotograph : System.Web.UI.UserControl
{
    private FlickrImage mDataSource = null;

    public FlickrImage DataSource
    {
        get { return this.mDataSource; }
        set { this.mDataSource = value; }
    }

    private void Page_Load(object sender, System.EventArgs e)
    {
    }

    private void Page_PreRender(object sender, System.EventArgs e)
    {
        if (this.mDataSource != null)
        {
            this.Visible = true;
            this.imgFlickrImage.AlternateText = this.mDataSource.Title;
            this.imgFlickrImage.ImageUrl = this.mDataSource.Urls["Medium"];
        }
        else
        {
            this.Visible = false;
        }
    }
}