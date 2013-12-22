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

public partial class wedding_Default : System.Web.UI.Page
{
    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.pnlBackground.Style["background-image"] = string.Format("url({0})", this.ResolveUrl("~/images/hawaii.jpg"));
    }
}
