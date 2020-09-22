using Beltzac.Account.Api.Models;
using Beltzac.Account.Domain;
using Beltzac.Account.Domain.Errors;
using Beltzac.Account.Domain.Interfaces;
using Beltzac.Account.Kernel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Beltzac.Account.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<Domain.Models.Account> _accounts;

        public AccountService(IRepository<Domain.Models.Account> accountRepository)
        {
            _accounts = accountRepository;
        }

        public Result ResetAccounts()
        {
            _accounts.DeleteAll();
            return new Result();
        }

        public Result<ProcessEventModel.Response> ProcessEvent(ProcessEventModel.Request eventModel)
        {
            return eventModel.Type switch
            {
                ProcessEventModel.EventType.Deposit => ProcessDepositEvent(eventModel),
                ProcessEventModel.EventType.Withdraw => ProcessWithdrawEvent(eventModel),
                ProcessEventModel.EventType.Transfer => ProcessTransferEvent(eventModel),
                _ => throw new NotImplementedException()
            };
        }

        private Result<ProcessEventModel.Response> ProcessDepositEvent(ProcessEventModel.Request eventModel)
        {
            var result = new Result<ProcessEventModel.Response>();

            Domain.Models.Account destination = _accounts.Get(eventModel.Destination);

            if (destination == null)
            {
                destination = new Domain.Models.Account(eventModel.Destination);
                _accounts.Add(destination);
            }

            destination.Deposit(eventModel.Amount);

            result.Data = new ProcessEventModel.Response(null, destination);
            return result;
        }

        private Result<ProcessEventModel.Response> ProcessWithdrawEvent(ProcessEventModel.Request eventModel)
        {
            var result = new Result<ProcessEventModel.Response>();

            Domain.Models.Account origin = _accounts.Get(eventModel.Origin);

            if (origin == null)
            {
                result.Errors.Add(new AccountNotFoundError());
            }
            else
            {
                origin.Withdraw(eventModel.Amount);
                result.Data = new ProcessEventModel.Response(origin, null);
            }

            return result;
        }

        private Result<ProcessEventModel.Response> ProcessTransferEvent(ProcessEventModel.Request eventModel)
        {
            var result = new Result<ProcessEventModel.Response>();

            Domain.Models.Account origin = _accounts.Get(eventModel.Origin);

            if (origin == null)
                result.Errors.Add(new AccountNotFoundError());
            else
            {
                Domain.Models.Account destination = _accounts.Get(eventModel.Destination);
                if (destination == null)
                {
                    destination = new Domain.Models.Account(eventModel.Destination);
                    _accounts.Add(destination);
                }

                origin.Transfer(destination, eventModel.Amount);

                result.Data = new ProcessEventModel.Response(origin, destination);
            }

            return result;
        }

        public Result<decimal> GetBalance(string id)
        {
            var result = new Result<decimal>();

            var account = _accounts.Get(id);

            if (account == null)
            {
                result.Errors.Add(new AccountNotFoundError());
            }
            else
            {
                result.Data = account.Balance;
            }

            return result;
        }
    }
}
