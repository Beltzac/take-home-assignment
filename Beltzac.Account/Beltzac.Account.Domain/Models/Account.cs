using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace Beltzac.Account.Domain.Models
{
    public class Account
    {
        public string Id { get; private set; }
        public decimal Balance => CalculateBalance();

        private readonly List<Transaction> _transactions;
        public IEnumerable<Transaction> Transactions => _transactions.AsReadOnly();

        public Account(string id)
        {
            Id = id;
            _transactions = new List<Transaction>();
        }

        public void Deposit(decimal amount)
        {
            AddTransaction(new DepositTransaction(amount));
        }

        public void Withdraw(decimal amount)
        {
            AddTransaction(new WithdrawTransaction(-amount));
        }

        public void Transfer(Account destination, decimal amount)
        {
            //From            
            AddTransaction(new TransferTransaction(-amount));

            //To            
            destination.AddTransaction(new TransferTransaction(amount));
        }

        private void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        private decimal CalculateBalance()
        {
            return _transactions.Sum(t => t.Amount);
        }
    }
}
