using System;
using System.Collections.Generic;
using System.Linq;

namespace Cliffy.Web.CliffordCorner
{
    public class PagePhotoSetTag : PageBase, IPagePhotographs
    {
        public List<IPhotograph> Photographs { get; set; }

        public string Tag { get; set; }

        public string Content { get; set; }
    }
}