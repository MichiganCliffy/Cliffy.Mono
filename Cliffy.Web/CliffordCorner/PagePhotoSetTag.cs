using System;
using System.Collections.Generic;
using System.Linq;
using Cliffy.Common;

namespace Cliffy.Web.CliffordCorner
{
    public class PagePhotoSetTag : PageBase, IPagePhotographs
    {
        public List<Photograph> Photographs { get; set; }

        public string Tag { get; set; }

        public string Content { get; set; }
    }
}