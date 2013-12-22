using System;
using System.Collections.Generic;
using System.Text;

namespace Cliffy.Web.CliffordCorner
{
    public class PageBlogPosts : PageBase
    {
        public string Tag { get; set; }

        public List<IBlogPost> Posts { get; set; }
    }
}