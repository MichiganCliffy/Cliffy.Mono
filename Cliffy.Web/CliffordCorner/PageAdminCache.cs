using System.Collections.Generic;

namespace Cliffy.Web.CliffordCorner
{
    public class PageAdminCache : PageAdmin
    {
        public List<string> CachedItems { get; set; }

        public PageAdminCache()
        {
            Id = "admin|cache";
        }
    }
}