using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
/// Default Landing Page.
/// </summary>
public partial class Default : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        //Server.Transfer(this.ResolveUrl("~/blogposts/default.aspx"));
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
    }
}