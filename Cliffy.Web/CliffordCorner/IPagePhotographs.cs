using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cliffy.Web.CliffordCorner
{
    /// <summary>
    /// Defines a page with a collection of photographs to show.
    /// </summary>
    public interface IPagePhotographs : IPage
    {
        List<IPhotograph> Photographs { get; set; }
    }
}
