using System;
using System.Collections.Generic;
using System.Text;
using Cliffy.Common;

namespace Cliffy.Web.CliffordCorner
{
    /// <summary>
    /// A content chapter that redirects to another set of pages or web site.
    /// </summary>
    public class PageRedirect : PageBase
    {
        /// <summary>
        /// Page / site to redirect to.
        /// </summary>
        public string RedirectTo { get; set; }
    }
}