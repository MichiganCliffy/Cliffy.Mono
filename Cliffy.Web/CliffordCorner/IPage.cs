using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cliffy.Web.CliffordCorner
{
    /// <summary>
    /// Defines the common contract that all CliffordCorner pages should implement. The Controller will expect, at a minimum, these fields.
    /// </summary>
    public interface IPage
    {
        /// <summary>
        /// The page title to use in the header.
        /// </summary>
        string MetaTitle { get; set; }

        /// <summary>
        /// The meta keywords to use in the header.
        /// </summary>
        string MetaKeywords { get; set; }

        /// <summary>
        /// Identifies the page. This should, combined with the parent id, provide a unique identifier for retrieving the page.
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Identifies the parent page.
        /// </summary>
        string ParentId { get; set; }

        /// <summary>
        /// The title to use when rendering the page.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Descriptive copy. This is could be the HTML that is shown on the page.
        /// </summary>
        string MetaDescription { get; set; }

        /// <summary>
        /// Identifies this page as a Travel Log page.
        /// </summary>
        bool IsTravelLog { get; set; }

        /// <summary>
        /// The type of page.
        /// </summary>
        PageType PageType { get; set; }

        /// <summary>
        /// Navigational links to other pages. This includes the global nav for the site, paging, and any subnavigational elements.
        /// </summary>
        Dictionary<NavigationType, PageLinks> Navigation { get; set; }
    }
}