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

public partial class Controls_WeddingMenu : System.Web.UI.UserControl
{
    private string mPage = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.mPage = Request.CurrentExecutionFilePath.ToLower();
        this.mPage = this.mPage.Substring(this.mPage.LastIndexOf("/") + 1);
    }

    protected void Page_PreRender(object sender, System.EventArgs e)
    {
        switch (this.mPage)
        {
            case "default.aspx":
                this.lnkHome.HRef = string.Empty;
                break;

            case "todo.aspx":
                this.lnkToDo.HRef = string.Empty;
                break;
        }
    }
}