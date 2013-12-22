using System;
using System.Collections.Generic;

namespace Cliffy.Web.UrlRewriter
{
    /// <summary>
    /// Collection of Rewriting rules.
    /// </summary>
    public class UrlRewriterRules : List<UrlRewriterRule>
    {
        private bool mEnabled = true;

        /// <summary>
        /// Are rewriting rules enabled?
        /// </summary>
        public bool Enabled
        {
            get { return this.mEnabled; }
            set { this.mEnabled = value; }
        }
    }
}