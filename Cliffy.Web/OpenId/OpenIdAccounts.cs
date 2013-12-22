using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.XPath;
using System.Web;
using Cliffy.Common.Caching;

namespace Cliffy.Web.OpenId
{
    public class OpenIdAccounts : List<OpenIdAccount>, IAccountRepository
    {
        public bool HasAccess(string openId)
        {
            return this.Any(x => String.Compare(openId, x.OpenId, StringComparison.OrdinalIgnoreCase) == 0);
        }
    }
}