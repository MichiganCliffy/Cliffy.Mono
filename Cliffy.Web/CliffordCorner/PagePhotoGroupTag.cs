using System;
using System.Collections.Generic;
using System.Linq;

namespace Cliffy.Web.CliffordCorner
{
    public class PagePhotoGroupTag : PageBase, IPagePhotographs
    {
        public string Tag { get; set; }
        public List<IPhotograph> Photographs { get; set; }
    }
}