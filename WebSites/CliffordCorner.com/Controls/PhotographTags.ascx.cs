using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
///	Hidden dialog box containing links of all tags associated with the photograph.
/// </summary>
public partial class Controls_PhotographTags : System.Web.UI.UserControl
{
    protected FlickrImage mDataSource = null;

    public virtual FlickrImage DataSource
    {
        get { return this.mDataSource; }
        set { this.mDataSource = value; }
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        // Put user code to initialize the page here
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (this.mDataSource != null)
        {
            if (this.mDataSource.Tags != null)
            {
                if (this.mDataSource.Tags.Length > 0)
                {
                    this.mTags.DataSource = this.mDataSource.Tags;
                    this.mTags.DataBind();
                }
                else
                {
                    this.Visible = false;
                }
            }
            else
            {
                this.Visible = false;
            }
        }
        else
        {
            this.Visible = false;
        }
    }

    protected void Tags_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        switch (e.Item.ItemType)
        {
            case ListItemType.AlternatingItem:
            case ListItemType.Item:
                string tag = (string)e.Item.DataItem;
                HtmlAnchor link = (HtmlAnchor)e.Item.FindControl("mTagUrl");

                link.InnerText = tag;
                break;
        }
    }
}