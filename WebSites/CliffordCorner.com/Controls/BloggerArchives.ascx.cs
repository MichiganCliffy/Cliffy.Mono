using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cliffy.Web.Blogger;

public partial class Controls_BloggerArchives : System.Web.UI.UserControl
{
    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.Visible = false;

        Blog blog = CliffyCache.GetBlog();
        if ((blog != null) && (blog.Archives.Count > 0))
        {
            this.Visible = true;

            this.rptArchives.DataSource = blog.Archives;
            this.rptArchives.DataBind();
        }
    }
}