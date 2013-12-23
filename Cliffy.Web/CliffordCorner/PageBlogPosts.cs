using System;
using System.Collections.Generic;
using System.Text;
using Cliffy.Common;

namespace Cliffy.Web.CliffordCorner
{
    public class PageBlogPosts : PageBase
    {
        public string Tag { get; set; }

        public List<BlogPost> Posts { get; set; }
    }
}