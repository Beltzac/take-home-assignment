using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beltzac.Account.Api.Models
{
    public class EventResponseModel
    {
        public AccountModel Origin { get; set; }
        public AccountModel Destination { get; set; }
    }
}
