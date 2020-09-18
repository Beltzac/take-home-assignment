using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Beltzac.Account.Domain
{
    public class Account
    {
        public int Id { get; private set; }
        public decimal Balance { get; private set; }

        public Account(int id)
        {
            Id = id;
            Balance = 0;
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            Balance -= amount;
        }
    }
}
