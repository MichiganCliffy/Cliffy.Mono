using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cliffy.Common;

namespace Cliffy.Web.CliffordCorner
{
    /// <summary>
    /// Defines a chapter that is made up of a set of flickr photoset.
    /// </summary>
    public class PagePhotoSetPhoto : PageBase
    {
        public Photograph Photograph { get; set; }

        public string Tag { get; set; }
    }
}