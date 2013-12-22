using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Cliffy.Web.CliffordCorner
{
    /// <summary>
    /// Defines a chapter that is made up of a flickr group.
    /// </summary>
    public class PagePhotoGroup : PageBase, IPagePhotographs
    {
        public List<IPhotograph> Photographs { get; set; }
    }
}