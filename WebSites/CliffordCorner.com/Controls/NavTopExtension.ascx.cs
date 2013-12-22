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
using Cliffy.Web.ContentEngine;

public partial class Controls_NavTopExtension : System.Web.UI.UserControl
{
    private ContentBook mBook = null;

    public ContentBook Book
    {
        get { return this.mBook; }
        set { this.mBook = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Visible = true;
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.Visible = false;
        if (this.mBook != null)
        {
            if (this.mBook.Chapters != null)
            {
                if (this.mBook.Chapters.Count > 0)
                {
                    this.Visible = true;
                    this.rptMoreTravelLogs.DataSource = this.mBook.Chapters.Values;
                    this.rptMoreTravelLogs.DataBind();
                }
            }
        }
    }
}
