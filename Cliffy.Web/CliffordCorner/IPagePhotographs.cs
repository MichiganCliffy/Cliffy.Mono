using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cliffy.Common;

namespace Cliffy.Web.CliffordCorner
{
    /// <summary>
    /// Defines a page with a collection of photographs to show.
    /// </summary>
    public interface IPagePhotographs : IPage
    {
        List<Photograph> Photographs { get; set; }
    }
}
