using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
///	Displays a list of tags from the Group Pool.
/// </summary>
public partial class Controls_Tags : System.Web.UI.UserControl
{
    private string[] mDataSource = null;
    private ArrayList mTags = new ArrayList();
    private int mCount = 5;
    private string mTitle = string.Empty;

    public int Count
    {
        get { return this.mCount; }
        set { this.mCount = value; }
    }

    public string Title
    {
        get { return this.mTitle; }
        set { this.mTitle = value; }
    }

    public string[] DataSource
    {
        get { return this.mDataSource; }
        set { this.mDataSource = value; }
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
                int endCount = this.mCount;
                if (endCount > this.mDataSource.Length)
                    endCount = this.mDataSource.Length;

                int counter = 0;
                while (counter < endCount)
                {
                    if (counter > this.mDataSource.Length) break;

                    if (this.mDataSource[counter].Length > 0)
                    {
                        this.mTags.Add(this.mDataSource[counter]);
                    }
                    else
                    {
                        endCount++;
                    }

                    counter++;
                }

                this.mLinks.DataSource = this.mTags;
                this.mLinks.DataBind();
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

    protected void Links_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        switch (e.Item.ItemType)
        {
            case ListItemType.Header:
                HtmlGenericControl title = (HtmlGenericControl)e.Item.FindControl("mTitle");
                title.InnerText = this.mTitle;
                break;

            case ListItemType.Item:
            case ListItemType.AlternatingItem:
                string tag = (string)e.Item.DataItem;
                HtmlAnchor link = (HtmlAnchor)e.Item.FindControl("mLink");
                link.HRef = this.ResolveUrl(string.Format("{0}/{1}", link.HRef, tag));
                link.InnerText = tag;
                break;

            case ListItemType.Footer:
                break;
        }
    }
}