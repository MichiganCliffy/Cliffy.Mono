using System;
using System.Collections.Generic;
using System.Linq;
using Cliffy.Common;

namespace Cliffy.Web.CliffordCorner
{
    public class PagePhotoGroupTag : PageBase, IPagePhotographs
    {
        public string Tag { get; set; }
        public List<Photograph> Photographs { get; set; }
    }
}