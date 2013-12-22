using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Controls_SimpleLink : System.Web.UI.UserControl
{
    private string mHref = string.Empty;

    public string Href
    {
        get { return this.mHref; }
        set { this.mHref = value; }
    }

    public string Title
    {
        get { return this.lnkTheLink.ToolTip; }
        set { this.lnkTheLink.ToolTip = value; }
    }

    public string Text
    {
        get { return this.lnkTheLink.Text; }
        set { this.lnkTheLink.Text = value; }
    }

    public string CssClass
    {
        get { return this.lnkTheLink.CssClass; }
        set { this.lnkTheLink.CssClass = value; }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.lnkTheLink.NavigateUrl = this.ResolveUrl(this.mHref);
    }
}