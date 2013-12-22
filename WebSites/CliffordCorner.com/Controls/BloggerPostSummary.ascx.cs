using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Cliffy.Web.Blogger;

public partial class Controls_BloggerPostSummary : System.Web.UI.UserControl
{
    private BlogPost mDataSource = null;

    public BlogPost DataSource
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

            this.lnkArticle.NavigateUrl = this.mDataSource.Link;
            this.lnkArticle.ToolTip = this.mDataSource.Title;
            this.lnkArticle.Text = this.mDataSource.Title;
        }
    }
}