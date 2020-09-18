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
        public int Origin { get; set; }
        public int Destination { get; set; }

        public enum EventType
        {
            Deposit,
            Withdraw,
            Transfer
        }
    }
}
