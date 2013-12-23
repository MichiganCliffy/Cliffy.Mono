using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.XPath;
using System.Text;
using Cliffy.Common;
using Cliffy.Common.Caching;

namespace Cliffy.Web.CliffordCorner
{
    public class ConfigReader
    {
		private readonly ICache mCache;

        public string CacheKey { get; set; }

        public int CacheDuration { get; set; }

        public string ConfigFile { get; set; }

        public ConfigReader() 
		{
			this.Initialize();
		}

		public ConfigReader(ICache cache) : this()
		{
			mCache = cache;
		}

        private void Initialize(string fileName = "ContentEngine.xml")
        {
            this.CacheKey = "Configurator";
            this.CacheDuration = 960;

            if (HttpContext.Current != null)
            {
                string path = string.Concat("~/App_Data/", fileName);
                this.ConfigFile = HttpContext.Current.Server.MapPath(path);
            }
        }

        public List<PageDefinition> Read()
        {
            List<PageDefinition> output = this.ReadFromCache();
            if (output == null)
            {
                output = this.ReadFromFile();
            }

            return output;
        }

        private List<PageDefinition> ReadFromCache()
        {
            List<PageDefinition> output = null;

            if (this.mCache != null)
            {
                if (this.mCache.IsInCache(this.CacheKey))
                {
                    this.mCache.GetFromCache<List<PageDefinition>>(this.CacheKey, out output);
                }
            }

            return output;
        }

        private List<PageDefinition> ReadFromFile()
        {
            List<PageDefinition> output = this.Read(this.ConfigFile);
            if ((output != null) && (this.mCache != null))
            {
                this.mCache.PutInCache<List<PageDefinition>>(this.CacheKey, this.CacheDuration, output);
            }

            return output;
        }

        public List<PageDefinition> Read(XmlNode node)
        {
            XPathNavigator reader = node.CreateNavigator();
            return this.Read(reader);
        }

        public List<PageDefinition> Read(string path)
        {
            List<PageDefinition> output = null;

            if (File.Exists(path))
            {
                XPathDocument db = new XPathDocument(path);
                XPathNavigator reader = db.CreateNavigator();

                if (reader.MoveToChild("contentBook", string.Empty))
                {
                    output = this.Read(reader);
                }
            }

            return output;
        }

        #region read in xml
        private List<PageDefinition> Read(XPathNavigator reader)
        {
            var output = new List<PageDefinition>();

            var children = reader.SelectChildren(XPathNodeType.Element);
            foreach (XPathNavigator child in children)
            {
                PageDefinition page = this.ReadPage(child);
                output.Add(page);
            }

            return output.OrderBy(x => x.SortOrder).ToList();
        }

        private bool AddOverview(XPathNavigator reader)
        {
            var output = true;

            var config = reader.GetAttribute("overview", string.Empty);
            if (!string.IsNullOrWhiteSpace(config))
            {
                if (String.Compare(config, "false", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    output = false;
                }
            }

            return output;
        }

        private PageDefinition ReadPage(XPathNavigator reader)
        {
            var output = new PageDefinition
            {
                IsTravelLog = true
            };

            this.AssignPageType(output, reader);
            this.AssignPageAttributes(output, reader);

            if (!string.IsNullOrWhiteSpace(output.Id))
            {
                XPathNodeIterator nodes = reader.SelectChildren(XPathNodeType.Element);
                foreach(XPathNavigator node in nodes)
                {
                    if (string.Compare(node.Name, "description", true) == 0)
                    {
                        output.MetaDescription = node.Value;
                    }
                    else if (string.Compare(node.Name, "pages", true) == 0)
                    {
                        var addOverview = AddOverview(node);

                        XPathNodeIterator children = node.SelectChildren(XPathNodeType.Element);
                        var sortOrder = 1;
                        foreach (XPathNavigator child in children)
                        {
                            if (string.Compare(child.Name, "menuTitle", true) == 0)
                            {
                                output.PageLinks.Title = child.Value;
                            }
                            else
                            {
                                var link = ReadPageLink(output.Id, child);
                                link.SortOrder = sortOrder;
                                if (string.IsNullOrWhiteSpace(link.MetaDescription))
                                {
                                    link.MetaDescription = output.MetaDescription;
                                }
                                output.PageLinks.Add(link);
                                sortOrder++;
                            }
                        }

                        if (addOverview)
                        {
                            var parent = new PageDefinition(output)
                            {
                                SortOrder = 0,
                                Title = "Overview"
                            };
                            output.PageLinks.Add(parent);
                        }
                    }
                    else
                    {
                        output.AddPageSetting(node.Name, node.Value);
                    }
                }

                if (output.PageLinks.Count > 0)
                {
                    if (string.IsNullOrWhiteSpace(output.PageLinks.Title))
                    {
                        output.PageLinks.Title = "Destinations";
                    }

                    // now we need to make sure that each page link knows about the other page links
                    foreach (var child in output.PageLinks)
                    {
                        child.PageLinks.Title = output.PageLinks.Title;
                        child.PageLinks.AddRange(output.PageLinks);
                        child.PageSettings.Add(output.PageSettings);
                    }
                }
            }

            return output;
        }

        private PageDefinition ReadPageLink(string parentId, XPathNavigator reader)
        {
            var output = new PageDefinition
            {
                ParentId = parentId
            };

            AssignPageType(output, reader);
            AssignPageAttributes(output, reader);

            if (!string.IsNullOrWhiteSpace(parentId))
            {
                output.Id = string.Concat(parentId, "|", output.Id);
            }

            if (!string.IsNullOrWhiteSpace(reader.InnerXml))
            {
                output.AddPageSetting("content", reader.InnerXml);
            }

            return output;
        }

        private void AssignPageType(PageDefinition page, XPathNavigator reader)
        {
            string name = reader.Name.ToLower();
            switch (name)
            {
                case "flickrset":
                    page.PageType = PageType.FlickrSet;
                    break;

                case "flickrtag":
                    page.PageType = PageType.FlickrSetTag;
                    break;

                case "htmlpage":
                    page.PageType = PageType.HtmlFragment;
                    break;

                case "redirect":
                    page.PageType = PageType.Redirect;
                    break;

                case "blogposts":
                    page.PageType = PageType.BlogPosts;
                    break;

                case "photographs":
                    page.PageType = PageType.FlickrGroup;
                    break;

                case "photographtag":
                    page.PageType = PageType.FlickrGroupTag;
                    break;

                case "home":
                    page.PageType = PageType.Home;
                    break;
            }
        }

        private void AssignPageAttributes(PageDefinition page, XPathNavigator reader)
        {
            if (reader.MoveToFirstAttribute())
            {
                page.AddPageSetting(reader.Name, reader.Value);
                while (reader.MoveToNextAttribute())
                {
                    page.AddPageSetting(reader.Name, reader.Value);
                }
            }

            reader.MoveToParent();
        }
        #endregion
    }
}