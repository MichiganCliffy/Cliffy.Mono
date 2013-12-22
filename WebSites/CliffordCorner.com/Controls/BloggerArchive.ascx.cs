using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cliffy.Web.Blogger;

public partial class Controls_BloggerArchive : System.Web.UI.UserControl
{
    private BlogArchive mDataSource = null;
    public BlogArchive DataSource
    {
        get { return this.mDataSource; }
        set { this.mDataSource = value; }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.Visible = false;

        if (this.mDataSource != null)
        {
            this.Visible = true;

            this.lnkArchive.NavigateUrl = string.Format("http://blog.cliffordcorner.com/{0}_{1:D2}_01_archive.html", this.mDataSource.Date.Year, this.mDataSource.Date.Month);
            this.lnkArchive.Text = string.Format("{0:MMMM} {1}", this.mDataSource.Date, this.mDataSource.Date.Year);
        }
    }
}