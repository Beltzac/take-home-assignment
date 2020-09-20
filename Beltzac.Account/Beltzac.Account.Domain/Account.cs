using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace Beltzac.Account.Domain
{
    public class Account
    {
        public int Id { get; private set; }
        public decimal Balance => CalculateBalance();
        public List<Transaction> Transactions { get; private set; }

        public Account(int id)
        {
            Id = id;
            Transactions = new List<Transaction>();
        }

        public void Deposit(decimal amount)
        {
            var transaction = new Transaction(Transaction.TransactionType.Deposit, amount);
            Transactions.Add(transaction);
        }

        public void Withdraw(decimal amount)
        {
            var transaction = new Transaction(Transaction.TransactionType.Withdraw, -amount);
            Transactions.Add(transaction);
        }

        public void Transfer(Account destination, decimal amount)
        {
            //From
            var transactionRemove = new Transaction(Transaction.TransactionType.Transfer, -amount);
            Transactions.Add(transactionRemove);

            //To
            var transactionAdd = new Transaction(Transaction.TransactionType.Transfer, amount);
            destination.Transactions.Add(transactionAdd);
        }

        private decimal CalculateBalance()
        {
            return Transactions.Sum(t => t.Amount);
        }
    }
}
