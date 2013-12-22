using System;
using System.Web;
using System.Web.UI;

namespace Cliffy.Web.UrlRewriter
{
    /// <summary>
    /// This makes sure that the action on the form maps to the right page.
    /// </summary>
    public class UrlRewriterHtmlTextWriter : HtmlTextWriter
    {
        private bool mInForm = false;
        private bool mInLink = false;
        private string mBaseUrl = string.Empty;
        private string mFormAction = string.Empty;

        public UrlRewriterHtmlTextWriter(HtmlTextWriter writer, HttpContext context) : base(writer)
        {
            string action = context.Request.CurrentExecutionFilePath;
            if (context.Request.QueryString.Count > 0) action += string.Concat("?", context.Request.QueryString);
            if (!string.IsNullOrEmpty(action))
            {
                this.mFormAction = action;
            }

            this.mBaseUrl = context.Request.ApplicationPath;
        }

        public override void RenderBeginTag(string tagName)
        {
            this.mInForm = string.Compare(tagName, "form", true) == 0;
            this.mInLink = string.Compare(tagName, "link", true) == 0;
            base.RenderBeginTag(tagName);
        }

        public override void RenderEndTag()
        {
            base.RenderEndTag();
            this.mInLink = false;
            this.mInForm = false;
        }

        public override void WriteAttribute(string name, string value, bool fEncode)
        {
            if (string.Compare(name, "action", true) == 0)
            {
                value = this.mFormAction;
            }
            else if (string.Compare(name, "href", true) == 0)
            {
                if (value.StartsWith("App_Themes", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (value.EndsWith(".css", StringComparison.CurrentCultureIgnoreCase))
                    {
                        value = string.Concat(this.mBaseUrl, "/", value);
                    }
                }
            }

            base.WriteAttribute(name, value, fEncode);
        }
    }
}