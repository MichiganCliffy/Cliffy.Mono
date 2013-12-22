using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cliffy.Web.Blogger;

public partial class Controls_BloggerPosts : System.Web.UI.UserControl
{
    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.Visible = false;

        Blog blog = CliffyCache.GetBlog();

        if ((blog != null) && (blog.Posts.Count > 0))
        {
            this.Visible = true;

            List<BlogPost> recent = new List<BlogPost>();

            // only want top 5
            int count = 0;
            foreach (BlogPost post in blog.Posts)
            {
                if (count >= 5) break;
                recent.Add(post);
                count++;
            }

            this.rptPosts.DataSource = recent;
            this.rptPosts.DataBind();
        }
    }
}