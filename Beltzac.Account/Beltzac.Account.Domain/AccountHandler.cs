using System;
using System.Collections.Generic;
using System.Text;

namespace Beltzac.Account.Domain
{
    public class AccountHandler : IAccountHandler
    {
        private readonly IRepository<Account> _accounts;

        public AccountHandler(IRepository<Account> accountRepository)
        {
            _accounts = accountRepository;
        }

        public void Reset()
        {
            var accounts = _accounts.GetAll();
            foreach (var a in accounts)
                DeleteAccount(a.Id);
        }

        public Account CreateAccount(int id)
        {
            var account = new Account(id);
            _accounts.Add(account);
            return account;
        }

        public void DeleteAccount(int id)
        {
            _accounts.Delete(id);
        }

        public Account GetAccount(int id)
        {
            return _accounts.Get(id);
        }

        public void Deposit(int idDestination, decimal amount)
        {
            var account = GetAccount(idDestination);

            //Create an account if it doesn't exists
            if (account == null)
                account = CreateAccount(idDestination);

            account.Deposit(amount);
        }

        public void Withdraw(int idOrigin, decimal amount)
        {
            var account = GetAccount(idOrigin);
            account.Withdraw(amount);
        }

        public void Transfer(int idOrigin, int idDestination, decimal amount)
        {
            var originAccount = GetAccount(idOrigin);
            originAccount.Withdraw(amount);

            var destinationAccount = GetAccount(idDestination);
            destinationAccount.Deposit(amount);
        }
    }
}
