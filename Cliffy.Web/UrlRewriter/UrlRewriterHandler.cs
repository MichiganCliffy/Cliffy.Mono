using System;
using System.Configuration;
using System.Text;
using System.Web;
using System.Xml;

namespace Cliffy.Web.UrlRewriter
{
    /// <summary>
    /// Config handler for the rewrite rules.
    /// </summary>
    public class UrlRewriterHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            UrlRewriterRules rules = new UrlRewriterRules();

            foreach (XmlAttribute attribute in section.Attributes)
            {
                if (string.Compare(attribute.Name, "enabled", true) == 0)
                {
                    if (string.Compare(attribute.Value, "false", true) == 0)
                    {
                        rules.Enabled = false;
                    }

                    continue;
                }
            }

            foreach (XmlNode child in section.ChildNodes)
            {
                if (string.Compare(child.Name, "rule", true) == 0)
                {
                    this.ProcessRule(rules, child);
                }
            }

            return rules;
        }

        private void ProcessRule(UrlRewriterRules rules, XmlNode section)
        {
            UrlRewriterRule rule = new UrlRewriterRule();

            StringBuilder source = new StringBuilder();

            foreach (XmlAttribute attribute in section.Attributes)
            {
                if (string.Compare(attribute.Name, "source", true) == 0)
                {
                    source.Append(attribute.Value);
                    continue;
                }

                if (string.Compare(attribute.Name, "destination", true) == 0)
                {
                    rule.Destination = attribute.Value;
                    continue;
                }

                if (string.Compare(attribute.Name, "skip", true) == 0)
                {
                    if (string.Compare(attribute.Value, "true", true) == 0)
                    {
                        rule.Skip = true;
                    }
                    continue;
                }
            }

            if (source.Length > 0)
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.ApplicationPath))
                {
                    if (HttpContext.Current.Request.ApplicationPath.IndexOf("CliffordCorner.com") >= 0)
                    {
                        source.Insert(0, "/");
                    }
                    source.Insert(0, HttpContext.Current.Request.ApplicationPath);
                }

                rule.Source = source.ToString();
            }

            if (rule.Source.Length > 0)
            {
                rules.Add(rule);
            }
        }
    }
}