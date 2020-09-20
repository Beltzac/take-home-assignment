using System;
using System.Collections.Generic;
using System.Text;

namespace Beltzac.Account.Domain
{
    public class Transaction
    {
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }

        public Transaction(TransactionType type, decimal amount)
        {
            Type = type;
            Amount = amount;
        }

        public enum TransactionType
        {
            Deposit,
            Withdraw,
            Transfer
        }
    }
}
