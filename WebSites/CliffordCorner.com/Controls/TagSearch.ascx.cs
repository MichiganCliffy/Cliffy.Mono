using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
///	Simple Control for displaying a Tag Search dialog box.
/// </summary>
public partial class Controls_TagSearch : System.Web.UI.UserControl
{
    protected string SearchText
    {
        get
        {
            if (Request.Form["t"] != null)
            {
                if (Request.Form["t"].Length > 0)
                {
                    return Request.Form["t"];
                }
            }

            return string.Empty;
        }
    }

    protected void Page_PreRender(object sender, System.EventArgs e)
    {
        this.btnSearch.OnClientClick = string.Format("location.href='{0}/Photographs/' + document.forms[0].t.value; return false;", this.Request.ApplicationPath);
    }
}