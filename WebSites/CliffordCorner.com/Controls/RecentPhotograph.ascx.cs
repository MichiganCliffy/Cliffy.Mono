using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Controls_RecentPhotograph : System.Web.UI.UserControl
{
    private FlickrImage mDataSource = null;

    public FlickrImage DataSource
    {
        get { return this.mDataSource; }
        set { this.mDataSource = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Visible = true;
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.Visible = false;
        if (this.mDataSource != null)
        {
            this.Visible = true;

            this.lnkPhotograph.NavigateUrl = string.Format(this.lnkPhotograph.NavigateUrl, this.mDataSource.Identifier, this.mDataSource.SecretID);
            this.lnkPhotograph.ToolTip = this.mDataSource.Title;

            this.imgPhotograph.ImageUrl = this.mDataSource.Urls["Thumbnail"];
            this.imgPhotograph.AlternateText = this.mDataSource.Title;
        }
    }
}