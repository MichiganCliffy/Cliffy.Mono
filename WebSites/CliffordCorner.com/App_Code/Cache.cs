using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using Cliffy.Web.Blogger;

public class CliffyCache
{
    public static Blog GetBlog()
    {
        HttpContext context = HttpContext.Current;

        if (context.Cache["BloggerPosts"] != null)
        {
            if (context.Cache["BloggerPosts"] is Blog)
            {
                return (Blog)context.Cache["BloggerPosts"];
            }
        }

        Blog blog = new Blog("http://blog.cliffordcorner.com/feeds/posts/default?alt=rss");
        context.Cache.Add("BloggerPosts", blog, null, Cache.NoAbsoluteExpiration, new TimeSpan(24, 0, 0), CacheItemPriority.Normal, null);
        return blog;
    }
}
