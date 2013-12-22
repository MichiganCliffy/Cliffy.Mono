using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace Cliffy.Web.CliffordCorner
{
    /// <summary>
    /// This defines a page for CliffordCorner. All pages described in the config file will be mapped to a page definition.
    /// </summary>
    public class PageDefinition : PageLink, ICloneable
    {
        /// <summary>
        /// Any page specific setting.
        /// </summary>
        public NameValueCollection PageSettings { get; set; }

        /// <summary>
        /// The links within the page.
        /// </summary>
        public PageLinks PageLinks { get; set; }

        public PageDefinition() : base()
        {
            this.PageType = PageType.Unknown;
            this.PageSettings = new NameValueCollection();
            this.PageLinks = new PageLinks();
        }

        public PageDefinition(PageDefinition source) : this()
        {
            this.Id = source.Id;
            this.IsTravelLog = source.IsTravelLog;
            this.MetaDescription = source.MetaDescription;
            this.ParentId = source.ParentId;
            this.SortOrder = source.SortOrder;
            this.Title = source.Title;
        }

        /// <summary>
        /// Utility method for adding a bunch of settings to the page.
        /// </summary>
        /// <param name="settings">The settings to add. This will not override any existing settings.</param>
        public void AddPageSettings(NameValueCollection settings)
        {
            string[] keys = settings.AllKeys;
            foreach (string key in keys)
            {
                if ((string.Compare(key, "id", true) == 0) ||
                    (string.Compare(key, "title", true) == 0) ||
                    (string.Compare(key, "number", true) == 0) ||
                    (string.Compare(key, "isTravelLog", true) == 0) ||
                    (string.Compare(key, "tag", true) == 0))
                {
                    continue;
                }
                else if (string.IsNullOrWhiteSpace(this.PageSettings[key]))
                {
                    this.AddPageSetting(key, settings[key]);
                }
            }
        }

        /// <summary>
        /// Add a page setting to the page. This will map settings to properties where appropriate.
        /// </summary>
        /// <param name="name">The name of the setting to map.</param>
        /// <param name="value">The value of the setting to map.</param>
        public void AddPageSetting(string name, string value)
        {
            string attributeName = name.ToLower();
            switch (attributeName)
            {
                case "title":
                    if (string.IsNullOrWhiteSpace(this.Id))
                    {
                        this.Id = PageDefinition.ToPageDefinitionId(value);
                    }
                    this.Title = value;
                    break;

                case "number":
                    int sortOrder = 0;
                    if (int.TryParse(value, out sortOrder))
                    {
                        this.SortOrder = sortOrder;
                    }
                    break;

                case "istravellog":
                    if (string.Compare(value, "false", true) == 0)
                    {
                        this.IsTravelLog = false;
                    }
                    break;

                case "tag":
                    if (string.IsNullOrWhiteSpace(this.Id))
                    {
                        this.Id = PageDefinition.ToPageDefinitionId(value);
                    }

                    if (string.IsNullOrWhiteSpace(this.PageSettings[name]))
                    {
                        this.PageSettings.Add(name, value);
                    }
                    break;

                default:
                    if (string.IsNullOrWhiteSpace(this.PageSettings[name]))
                    {
                        this.PageSettings.Add(name, value);
                    }
                    break;
            }
        }

        public void CopyTo(IPage target)
        {
            if (target != null)
            {
                target.Id = this.Id;
                target.IsTravelLog = this.IsTravelLog;
                target.ParentId = this.ParentId;
                target.Title = this.Title;
                target.MetaDescription = this.MetaDescription;
                target.PageType = this.PageType;
            }
        }

        public object Clone()
        {
            return new PageDefinition()
            {
                MetaDescription = this.MetaDescription,
                Id = this.Id,
                PageType = this.PageType,
                ParentId = this.ParentId,
                IsTravelLog = this.IsTravelLog,
                SortOrder = this.SortOrder,
                Title = this.Title,
                PageLinks = this.PageLinks
            };
        }

        public static string ToPageDefinitionId(string parent, string child)
        {
            StringBuilder output = new StringBuilder(parent.ToLower());
            output.Append("|");
            output.Append(child.ToLower());
            output.Replace(' ', '-');
            return output.ToString();
        }

        public static string ToPageDefinitionId(string source)
        {
            StringBuilder output = new StringBuilder(source.ToLower());
            output.Replace(' ', '-');
            return output.ToString();
        }
    }
}