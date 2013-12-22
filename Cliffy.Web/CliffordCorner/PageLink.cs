using System;

namespace Cliffy.Web.CliffordCorner
{
    /// <summary>
    /// A link to a page within CliffordCorner. This class contains the basic properties that all configured pages within CliffordCorner should contain.
    /// </summary>
    public abstract class PageLink
    {
        /// <summary>
        /// A unique identifier for the page.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The pages parent identifier.
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// Identifies the type of page being defined.
        /// </summary>
        public PageType PageType { get; set; }

        /// <summary>
        /// The sort order of the page within a list of pages (e.g. the global nav, or sub nav).
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// The page title.
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// Descriptive content for the page.
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Is this page a travellog?
        /// </summary>
        public bool IsTravelLog { get; set; }

        public PageLink()
        {
            this.SortOrder = 0;
            this.IsTravelLog = false;
        }
    }
}
