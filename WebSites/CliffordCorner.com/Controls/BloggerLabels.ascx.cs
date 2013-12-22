using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cliffy.Web.Blogger;

public partial class Controls_BloggerLabels : System.Web.UI.UserControl
{
    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.Visible = false;

        Blog blog = CliffyCache.GetBlog();
        if ((blog != null) && (blog.Labels.Count > 0))
        {
            this.Visible = true;

            // only want the top 10
            List<BlogLabel> topLabels = new List<BlogLabel>();

            int count = 0;
            foreach (BlogLabel label in blog.Labels)
            {
                if (count >= 10) break;
                topLabels.Add(label);
                count++;
            }

            this.rptLabels.DataSource = topLabels;
            this.rptLabels.DataBind();
        }
    }
}