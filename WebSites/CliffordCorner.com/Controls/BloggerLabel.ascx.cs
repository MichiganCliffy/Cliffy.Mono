using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cliffy.Web.Blogger;

public partial class Controls_BloggerLabel : System.Web.UI.UserControl
{
    private BlogLabel mDataSource = null;
    public BlogLabel DataSource
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

            this.lnkLabel.NavigateUrl = string.Concat("http://blog.cliffordcorner.com/search/label/", this.mDataSource.Label);
            this.lnkLabel.Text = this.mDataSource.Label;
        }
    }
}