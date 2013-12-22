using System;

namespace Cliffy.Web.UrlRewriter
{
    /// <summary>
    /// Defines a rule for re-writing a url.
    /// </summary>
    public class UrlRewriterRule
    {
        private string mSource = string.Empty;
        private string mDestination = string.Empty;
        private bool mSkip = false;

        /// <summary>
        /// The source url (regex expression).
        /// </summary>
        public string Source
        {
            get { return this.mSource; }
            set { this.mSource = value; }
        }

        /// <summary>
        /// The destination url to go to.
        /// </summary>
        public string Destination
        {
            get { return this.mDestination; }
            set { this.mDestination = value; }
        }

        /// <summary>
        /// Do not process this rule
        /// </summary>
        public bool Skip
        {
            get { return this.mSkip; }
            set { this.mSkip = value; }
        }
    }
}