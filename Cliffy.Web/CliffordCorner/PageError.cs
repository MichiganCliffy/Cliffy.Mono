using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cliffy.Web.CliffordCorner
{
    public class PageError : PageBase
    {
        public string RequestedUrl { get; set; }

        public string ReferingUrl { get; set; }
    }
}