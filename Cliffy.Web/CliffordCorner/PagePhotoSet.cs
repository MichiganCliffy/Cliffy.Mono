using System;
using System.Collections.Generic;

namespace Cliffy.Web.CliffordCorner
{
    public class PagePhotoSet : PageBase
    {
        public IPhotograph Spotlight { get; set; }

        public List<IPhotograph> Photographs { get; set; }

        /// <summary>
        /// A description of the photo set that is returned from Flickr or the photo set source.
        /// </summary>
        public string Content { get; set; }
    }
}