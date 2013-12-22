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

namespace Cliffy.Web.OpenId
{
    public class ConfigReader : BaseMgr
    {
        public class Constants
        {
            public const string CacheKeyDefault = "OpenIdUsers";
            public const int CacheDurationDefault = 960;
        }

        public string CacheKey { get; set; }

        public int CacheDuration { get; set; }

        public string ConfigFile { get; set; }

        public ConfigReader(string fileName = null) : base() { this.Initialize(fileName); }
        public ConfigReader(ICache cache, string fileName = null) : base(cache) { this.Initialize(fileName); }

        private void Initialize(string fileName)
        {
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                CacheKey = Constants.CacheKeyDefault;
                CacheDuration = Constants.CacheDurationDefault;

                if (HttpContext.Current != null)
                {
                    var path = string.Concat("~/App_Data/", fileName);
                    ConfigFile = HttpContext.Current.Server.MapPath(path);
                }
                else
                {
                    ConfigFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                }
            }
        }

        public OpenIdAccounts Load()
        {
            OpenIdAccounts output = ReadFromCache();
            if (output == null)
            {
                output = ReadFromFile();
            }

            return output;
        }

        private OpenIdAccounts ReadFromCache()
        {
            OpenIdAccounts output = null;

            if (mCache != null)
            {
                mCache.GetFromCache(CacheKey, out output);
            }

            return output;
        }

        private OpenIdAccounts ReadFromFile()
        {
            OpenIdAccounts output = Read(ConfigFile);
            if ((output != null) && (mCache != null))
            {
                mCache.PutInCache(CacheKey, CacheDuration, output);
            }

            return output;
        }

        public OpenIdAccounts Read(XmlNode node)
        {
            XPathNavigator reader = node.CreateNavigator();
            return Read(reader);
        }

        public OpenIdAccounts Read(string path)
        {
            OpenIdAccounts output = null;

            if (File.Exists(path))
            {
                XPathDocument db = new XPathDocument(path);
                XPathNavigator reader = db.CreateNavigator();

                if (reader.MoveToChild("users", string.Empty))
                {
                    output = Read(reader);
                }
            }

            return output;
        }

        private OpenIdAccounts Read(XPathNavigator reader)
        {
            var output = new OpenIdAccounts();

            var children = reader.SelectChildren(XPathNodeType.Element);
            foreach (XPathNavigator child in children)
            {
                OpenIdAccount user = new OpenIdAccount();
                user.OpenId = child.Value;
                output.Add(user);
            }

            return output;
        }
    }
}