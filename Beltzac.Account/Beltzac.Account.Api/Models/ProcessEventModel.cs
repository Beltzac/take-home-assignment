using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beltzac.Account.Api.Models
{
    public class ProcessEventModel
    {
        public class Request
        {
            public EventType Type { get; set; }
            public decimal Amount { get; set; }
            public string Origin { get; set; }
            public string Destination { get; set; }
        }

        public class Response
        {
            public AccountModel Origin { get; set; }
            public AccountModel Destination { get; set; }

            public Response(Domain.Models.Account origin, Domain.Models.Account destination)
            {
                if (origin != null)
                    Origin = new AccountModel { Id = origin.Id, Balance = origin.Balance };

                if (destination != null)
                    Destination = new AccountModel { Id = destination.Id, Balance = destination.Balance };
            }
        }

        public enum EventType
        {
            Deposit,
            Withdraw,
            Transfer
        }

        public class AccountModel
        {
            public string Id { get; set; }
            public decimal Balance { get; set; }
        }
    }
}
