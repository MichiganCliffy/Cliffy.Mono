using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Cliffy.Web.OpenId
{
    public class OpenIdAccountsFromConfig : OpenIdAccounts, IAccountRepository
    {
        public OpenIdAccountsFromConfig(string section = "OpenIdAccounts")
        {
            var fromConfig = (OpenIdAccounts)ConfigurationManager.GetSection(section);
            if (fromConfig != null)
            {
                this.AddRange(fromConfig);
            }
        }
    }
}
