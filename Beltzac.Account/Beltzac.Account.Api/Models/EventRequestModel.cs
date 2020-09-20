using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beltzac.Account.Api.Models
{
    public class EventRequestModel
    {
        public EventType Type { get; set; }
        public decimal Amount { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }

        public enum EventType
        {
            Deposit,
            Withdraw,
            Transfer
        }
    }
}
