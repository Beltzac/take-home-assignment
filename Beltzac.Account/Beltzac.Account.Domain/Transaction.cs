using System;
using System.Collections.Generic;
using System.Text;

namespace Beltzac.Account.Domain
{
    public abstract class Transaction
    {
        public decimal Amount { get; set; }

        public Transaction(decimal amount)
        {
            Amount = amount;
        }
    }

    public class DepositTransaction : Transaction
    {
        public DepositTransaction(decimal amount) : base(amount)
        {
        }
    }

    public class WithdrawTransaction : Transaction
    {
        public WithdrawTransaction(decimal amount) : base(amount)
        {
        }
    }

    public class TransferTransaction : Transaction
    {
        public TransferTransaction(decimal amount) : base(amount)
        {
        }
    }
}
