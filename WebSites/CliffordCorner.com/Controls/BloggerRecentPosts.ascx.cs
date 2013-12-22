using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Caching;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cliffy.Web.Blogger;

/// <summary>
///	Most Recent Blogs from Blogger.
/// </summary>
public partial class Controls_BloggerRecentPosts : System.Web.UI.UserControl
{
    private int mCount = 5;

    public int Count
    {
        get { return this.mCount; }
        set { this.mCount = value; }
    }

    protected void Page_PreRender(object sender, System.EventArgs e)
    {
        this.Visible = false;

        Blog blog = CliffyCache.GetBlog();

        if ((blog != null) && (blog.Posts.Count > 0))
        {
            this.Visible = true;

            List<BlogPost> recent = new List<BlogPost>();

            int count = 0;
            foreach (BlogPost post in blog.Posts)
            {
                if (count >= this.mCount) break;
                recent.Add(post);
                count++;
            }

            this.rptPosts.DataSource = recent;
            this.rptPosts.DataBind();
        }
    }
}