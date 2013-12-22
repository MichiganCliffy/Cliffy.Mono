using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cliffy.Common
{
    /// <summary>
    /// Provides the URI for a photograph on the web.
    /// </summary>
    public class PhotographUri
    {
        /// <summary>
        /// The photograph size.
        /// </summary>
		public ImageSize Size { get; set; }

        /// <summary>
        /// The URI to the photograph.
        /// </summary>
        public string Uri { get; set; }
    }
}