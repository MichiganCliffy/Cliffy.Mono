using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cliffy.Web
{
    public interface IAccountRepository
    {
        bool HasAccess(string account);
    }
}
