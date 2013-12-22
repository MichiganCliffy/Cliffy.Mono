using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Cliffy.Common.Caching;

namespace Cliffy.Web.OpenId
{
    public class OpenIdAccountsFromXmlFile : OpenIdAccounts, IAccountRepository
    {
        public OpenIdAccountsFromXmlFile(ICache cache, string fileName = "Users.dat")
        {
            var loader = new ConfigReader(cache, fileName);
            var fromFile = loader.Load();
            if (fromFile != null)
            {
                this.AddRange(fromFile);
            }
        }
    }
}
