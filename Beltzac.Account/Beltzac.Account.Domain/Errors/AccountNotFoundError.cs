using Beltzac.Account.Kernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beltzac.Account.Domain.Errors
{
    public class AccountNotFoundError : Error
    {
        public AccountNotFoundError() : base(null)
        {
        }

        public AccountNotFoundError(string message) : base(message)
        {
        }
    }
}
