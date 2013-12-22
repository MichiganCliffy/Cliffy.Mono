using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Cliffy.Web.UrlRewriter
{
    /// <summary>
    /// Url re-writer.
    /// </summary>
    public class UrlRewriterHttpModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
            context.PreRequestHandlerExecute += new EventHandler(context_PreRequestHandlerExecute);
        }

        void context_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            if ((app.Context.CurrentHandler is Page) &&
                (app.Context.CurrentHandler != null))
            {
                Page pg = (Page)app.Context.CurrentHandler;
                pg.PreInit += new EventHandler(Page_PreInit);
            }
        }

        void Page_PreInit(object sender, EventArgs e)
        {
            // restore internal path to original
            // this is required to handle postbacks
            if (HttpContext.Current.Items.Contains("OriginalUrl"))
            {
                string path = (string)HttpContext.Current.Items["OriginalUrl"];
                if (path.IndexOf("?") == -1) path += "?";
                HttpContext.Current.RewritePath(path);
            }
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            UrlRewriterRules rules = (UrlRewriterRules)ConfigurationManager.GetSection("rewriteModule");
            if (!rules.Enabled) return;

            string path = HttpContext.Current.Request.Path;
            if (path.Length == 0) return;

            string ext = path.Substring(path.LastIndexOf(".") + 1).ToLower();
            switch (ext)
            {
                case "css":
                case "js":
                case "jpg":
                case "gif":
                case "aspx":
                case "html":
                case "asp":
                    return;
            }

            foreach (UrlRewriterRule rule in rules)
            {
                Regex regex = new Regex(rule.Source, RegexOptions.IgnoreCase);
                Match match = regex.Match(path);
                if (match.Success)
                {
                    if (rule.Skip) return;

                    path = regex.Replace(path, rule.Destination);

                    if (path.Length > 0)
                    {
                        StringBuilder queryString = new StringBuilder();
                        foreach (string key in HttpContext.Current.Request.QueryString.Keys)
                        {
                            if (queryString.Length > 0) queryString.Append("&");
                            queryString.AppendFormat("{0}={1}", key, HttpContext.Current.Request.QueryString[key]);
                        }
                        if (queryString.Length > 0) path += string.Format("?{0}", queryString);

                        HttpContext.Current.RewritePath(string.Format("{0}{1}", HttpContext.Current.Request.ApplicationPath, path));
                        return;
                    }
                }
            }
        }
    }
}