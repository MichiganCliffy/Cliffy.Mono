using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Cliffy.Web.UrlRewriter
{
    /// <summary>
    /// A base page to use for all pages that are returned from the url rewriter. It make's sure that the form actions are directed to the correct page.
    /// </summary>
    public class UrlRewriterBasePage : System.Web.UI.Page
    {
        protected override void Render(HtmlTextWriter writer)
        {
            writer = new UrlRewriterHtmlTextWriter(writer, this.Context);
            base.Render(writer);
        }
    }
}
