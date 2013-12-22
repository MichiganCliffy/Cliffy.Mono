using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_BloggerPostLabel : System.Web.UI.UserControl
{
    private string mDataSource = string.Empty;

    public string DataSource
    {
        get { return this.mDataSource; }
        set { this.mDataSource = value; }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.Visible = false;
        if (!string.IsNullOrEmpty(this.mDataSource))
        {
            this.Visible = true;

            this.lnkLabel.Text = this.mDataSource;
            this.lnkLabel.NavigateUrl = string.Concat("http://blog.cliffordcorner.com/search/label/", this.mDataSource);
            this.lnkLabel.ToolTip = string.Format("View other posts with the {0} label", this.mDataSource);
        }
    }
}