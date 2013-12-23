using System;
using System.Collections.Generic;
using Cliffy.Common;

namespace Cliffy.Web.CliffordCorner
{
    public class PagePhotoSet : PageBase
    {
        public Photograph Spotlight { get; set; }

        public List<Photograph> Photographs { get; set; }

        /// <summary>
        /// A description of the photo set that is returned from Flickr or the photo set source.
        /// </summary>
        public string Content { get; set; }
    }
}