using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cliffy.Web.UrlRewriter;

/// <summary>
/// Redirects to appropriate page, if exists.
/// </summary>
public partial class _404 : System.Web.UI.Page
{
    private UrlRewriterRules mRewriter = null;
    private string mRequest = string.Empty;

    protected void Page_Load(object sender, System.EventArgs e)
    {
        Trace.Write("aspx.page", "Page_Load");
        this.mRewriter = (UrlRewriterRules)ConfigurationManager.GetSection("rewriteModule");
        this.mRequest = this.RenderRequest();
    }

    protected void Page_PreRender(object sender, System.EventArgs e)
    {
        Trace.Write("aspx.page", "Page_PreRender");
        if (this.mRewriter.Enabled)
        {
            if (this.mRequest.Length > 0)
            {
                string ext = this.mRequest.Substring(this.mRequest.LastIndexOf(".") + 1);
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

                foreach (UrlRewriterRule rule in this.mRewriter)
                {
                    Trace.Write("aspx.page", string.Format("matching {0} and {1}", rule.Source, this.mRequest));
                    Regex regex = new Regex(rule.Source, RegexOptions.IgnoreCase);
                    Match match = regex.Match(this.mRequest);
                    Trace.Write("aspx.page", string.Format("match is {0}", match.Success));
                    if (match.Success)
                    {
                        if (rule.Skip) return;

                        string path = regex.Replace(this.mRequest, rule.Destination);

                        if (path.Length > 0)
                        {
                            StringBuilder queryString = new StringBuilder();
                            NameValueCollection queryStringValues = this.RenderQueryString();
                            foreach (string key in queryStringValues.Keys)
                            {
                                Trace.Write("aspx.page", string.Format("key is {0} and value is {1}", key, queryStringValues[key]));
                                if (queryString.Length > 0) queryString.Append("&");
                                queryString.AppendFormat("{0}={1}", key, queryStringValues[key]);
                            }
                            if (queryString.Length > 0) path += string.Format("&{0}", queryString);

                            Trace.Write("aspx.page", this.ResolveUrl(string.Format("~{0}", path)));
                            Server.Transfer(this.ResolveUrl(string.Format("~{0}", path)));
                            return;
                        }
                    }
                }
            }
        }

        // if we got here, we have a true 404
        Server.Transfer(this.ResolveUrl("~/404Message.aspx"));
    }

    private string RenderRequest()
    {
        StringBuilder request = new StringBuilder(Server.UrlDecode(Request.QueryString.ToString()));
        request.Replace("404;", string.Empty);
        request.Replace(":80", string.Empty);
        request.Replace("http://", string.Empty);
        request.Replace(this.Request.ServerVariables["SERVER_NAME"], string.Empty);
        //request.Replace(this.Request.ApplicationPath, string.Empty);

        Trace.Write("aspx.page", request.ToString());
        string output = request.ToString();
        if (output.IndexOf("?") > 0)
            output = output.Substring(0, output.IndexOf("?"));
        return output.ToLower();
    }

    private NameValueCollection RenderQueryString()
    {
        NameValueCollection output = new NameValueCollection();

        StringBuilder request = new StringBuilder(Server.UrlDecode(Request.QueryString.ToString()));
        request.Replace("404;", "");
        request.Replace(":80", "");
        request.Replace("http://", "");
        request.Replace(this.Request.ServerVariables["SERVER_NAME"], "");

        string querystring = request.ToString();
        if (querystring.IndexOf("?") > 0)
        {
            querystring = querystring.Substring(querystring.IndexOf("?") + 1);
            Trace.Write("aspx.page", string.Format("querystring is {0}", querystring));

            string[] parameters = querystring.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string parameter in parameters)
            {
                string[] nameValue = parameter.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                if (nameValue.Length == 2)
                {
                    output.Add(nameValue[0], nameValue[1]);
                }
            }
        }

        return output;
    }
}