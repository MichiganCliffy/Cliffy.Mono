using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cliffy.Web.CliffordCorner
{
    /// <summary>
    /// A grouping of page links with a common theme.
    /// </summary>
    public class PageLinks : List<PageDefinition>, ICloneable
    {
        /// <summary>
        /// The title that describes the theme of the group of links.
        /// </summary>
        public string Title { get; set; }

        #region ICloneable Members

        public object Clone()
        {
            PageLinks output = new PageLinks();

            foreach (var item in this)
            {
                var clone = (PageDefinition)item.Clone();
                output.Add(clone);
            }

            return output;
        }

        #endregion
    }
}