using System;
using System.Configuration;
using System.Xml;

namespace Cliffy.Web.OpenId
{
    /// <summary>
    /// Config handler for site.
    /// </summary>
    public class ConfigHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            ConfigReader reader = new ConfigReader();
            return reader.Read(section);
        }
    }
}