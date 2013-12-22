using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cliffy.Web.CliffordCorner
{
    /// <summary>
    /// This is a image that links to a page. It contains all of the same properties that a PageLink has, but includes URIs to the image being shown.
    /// </summary>
    public class ThumbnailLink : PageLink, ICloneable
    {
        /// <summary>
        /// The pointer to the thumbnail image.
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Is this a thumbnail for a video?
        /// </summary>
        public bool IsVideo { get; set; }

        public ThumbnailLink() : base()
        {
            this.IsVideo = false;
        }

        public object Clone()
        {
            ThumbnailLink output = new ThumbnailLink()
            {
                MetaDescription = this.MetaDescription,
                Id = this.Id,
                IsTravelLog = this.IsTravelLog,
                IsVideo = this.IsVideo,
                ParentId = this.ParentId,
                SortOrder = this.SortOrder,
                Title = this.Title,
                Uri = this.Uri
            };

            return output;
        }
    }
}