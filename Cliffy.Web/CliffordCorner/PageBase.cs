using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cliffy.Web.CliffordCorner
{
    /// <summary>
    /// The common properties that all site pages have. This implements the core ISitePage properties.
    /// </summary>
    public class PageBase : PageLink, IPage
    {
        /// <summary>
        /// The page title to use in the header.
        /// </summary>
        public string MetaTitle { get; set; }

        /// <summary>
        /// The meta keywords to use in the header.
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Links to other pages within this chapter. There could be multiple areas within the chapter, this segments them by using a unique key for each segment.
        /// </summary>
        public Dictionary<NavigationType, PageLinks> Navigation { get; set; }
    }
}