using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cliffy.Web.CliffordCorner
{
    public enum NavigationType
    {
        /// <summary>
        /// Identifies the global navigation elements.
        /// </summary>
        Global,

        /// <summary>
        /// The standard type of sub navigation. Common across all sections of the site.
        /// </summary>
        Sub,

        /// <summary>
        /// Identifies all of the pages within a section (chapter) of the site.
        /// </summary>
        Paging,

        /// <summary>
        /// Popular photo tags.
        /// </summary>
        PopularTags,

        /// <summary>
        /// Tags that are associated with an image.
        /// </summary>
        ImageTags,

        /// <summary>
        /// The most recent posts.
        /// </summary>
        RecentPosts,

        /// <summary>
        /// Links to the blog archive.
        /// </summary>
        ArchivedPosts
    }
}