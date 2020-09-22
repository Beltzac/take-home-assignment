using Beltzac.Account.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beltzac.Account.UnitTest.Domain.Models
{
    public class AccountTest
    {
        [TestClass]
        public class DepositMethod
        {
            [TestMethod]
            public void AddsTransaction()
            {
                //Arrange
                decimal amount = 50;
                var account = new Account.Domain.Models.Account("10");

                //Act
                account.Deposit(amount);

                //Assert
                var transaction =  account.Transactions.OfType<DepositTransaction>().First();
                Assert.IsNotNull(transaction);
                Assert.AreEqual(amount, transaction.Amount);
            }


            [TestMethod]
            public void AffectsBalance()
            {
                //Arrange
                decimal amount = 20;
                decimal amount2 = 70;
                var account = new Account.Domain.Models.Account("10");

                //Act
                account.Deposit(amount);
                account.Deposit(amount2);

                //Assert 
                Assert.AreEqual(amount + amount2, account.Balance);
            }           
        }

        [TestClass]
        public class WithdrawMethod
        {
            [TestMethod]
            public void AddsTransaction()
            {
                //Arrange
                decimal amount = 50;
                var account = new Account.Domain.Models.Account("10");

                //Act
                account.Withdraw(amount);

                //Assert
                var transaction = account.Transactions.OfType<WithdrawTransaction>().First();
                Assert.IsNotNull(transaction);
                Assert.AreEqual(-amount, transaction.Amount);
            }


            [TestMethod]
            public void AffectsBalance()
            {
                //Arrange
                decimal amount = 20;
                decimal amount2 = 70;
                var account = new Account.Domain.Models.Account("10");

                //Act
                account.Withdraw(amount);
                account.Withdraw(amount2);

                //Assert 
                Assert.AreEqual(-amount - amount2, account.Balance);
            }
        }

        [TestClass]
        public class TransferMethod
        {
            [TestMethod]
            public void AddsTransaction()
            {
                //Arrange
                decimal amount = 50;
                var origin = new Account.Domain.Models.Account("10");
                var destination = new Account.Domain.Models.Account("11");

                //Act
                origin.Transfer(destination, amount);

                //Assert
                var transaction1 = origin.Transactions.OfType<TransferTransaction>().First();
                Assert.IsNotNull(transaction1);
                Assert.AreEqual(-amount, transaction1.Amount);

                var transaction2 = destination.Transactions.OfType<TransferTransaction>().First();
                Assert.IsNotNull(transaction2);
                Assert.AreEqual(amount, transaction2.Amount);
            }


            [TestMethod]
            public void AffectsBalance()
            {
                //Arrange
                decimal amount1 = 50;
                decimal amount2 = 40;
                var origin = new Account.Domain.Models.Account("10");
                var destination = new Account.Domain.Models.Account("11");

                //Act
                origin.Transfer(destination, amount1);
                destination.Transfer(origin, amount2);

                //Assert
                Assert.AreEqual(-10, origin.Balance);
                Assert.AreEqual(10, destination.Balance);
            }
        }
    }
}
