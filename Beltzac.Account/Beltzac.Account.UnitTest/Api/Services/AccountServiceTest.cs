using Beltzac.Account.Api.Models;
using Beltzac.Account.Api.Services;
using Beltzac.Account.Domain.Errors;
using Beltzac.Account.Domain.Models;
using Beltzac.Account.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Beltzac.Account.UnitTest.Api.Services
{
    public class AccountServiceTest
    {
        [TestClass]
        public class ProcessEventMethod
        {
            [TestMethod]
            public void DepositCreatesDestinationIfInexistent()
            {
                //Arrange
                string id = "10";
                var repo = new AccountRepository();
                var service = new AccountService(repo);
                var eventModel = new ProcessEventModel.Request
                {
                    Type = ProcessEventModel.EventType.Deposit,
                    Destination = id,
                    Amount = 10
                };

                //Act
                service.ProcessEvent(eventModel);

                //Assert
                var account = repo.Get(id);
                Assert.IsNotNull(account);
            }

            [TestMethod]
            public void TransferCreatesDestinationIfInexistent()
            {
                //Arrange
                string idOrigin = "10";
                string idDestination = "11";
                var repo = new AccountRepository();
                var service = new AccountService(repo);
                var eventModel = new ProcessEventModel.Request
                {
                    Type = ProcessEventModel.EventType.Transfer,
                    Origin = idOrigin,
                    Destination = idDestination,
                    Amount = 10
                };

                repo.Add(new Account.Domain.Models.Account(idOrigin));

                //Act
                service.ProcessEvent(eventModel);

                //Assert
                var account = repo.Get(idDestination);
                Assert.IsNotNull(account);
            }

            [TestMethod]
            public void WithdrawErrorsIfOriginNotFound()
            {
                //Arrange    
                var repo = new AccountRepository();
                var service = new AccountService(repo);
                var eventModel = new ProcessEventModel.Request
                {
                    Type = ProcessEventModel.EventType.Withdraw,
                    Origin = "400",
                    Amount = 10
                };

                //Act
                var result = service.ProcessEvent(eventModel);

                //Assert
                Assert.IsFalse(result.IsOK);
                Assert.IsTrue(result.Errors.OfType<AccountNotFoundError>().Any());
            }

            [TestMethod]
            public void TransferErrorsIfOriginNotFound()
            {
                //Arrange    
                string idDestination = "10";
                var repo = new AccountRepository();
                var service = new AccountService(repo);
                var eventModel = new ProcessEventModel.Request
                {
                    Type = ProcessEventModel.EventType.Transfer,
                    Origin = "500",
                    Destination = idDestination,
                    Amount = 10
                };

                repo.Add(new Account.Domain.Models.Account(idDestination));

                //Act
                var result = service.ProcessEvent(eventModel);

                //Assert
                Assert.IsFalse(result.IsOK);
                Assert.IsTrue(result.Errors.OfType<AccountNotFoundError>().Any());
            }
        }


        public class GetBalanceMethod
        {

            [TestMethod]
            public void ErrorsIfAcountNotExists()
            {
                //Arrange    
                string id = "10";
                var repo = new AccountRepository();
                var service = new AccountService(repo);

                //Act
                var result = service.GetBalance(id);

                //Assert
                Assert.IsFalse(result.IsOK);
                Assert.IsTrue(result.Errors.OfType<AccountNotFoundError>().Any());
            }

            [TestMethod]
            public void ReturnsAccountBalance()
            {
                //Arrange    
                string id = "10";
                decimal value = 60;
                var repo = new AccountRepository();
                var service = new AccountService(repo);
                var account = new Account.Domain.Models.Account(id);

                repo.Add(account);
                account.Deposit(value);

                //Act
                var result = service.GetBalance(id);

                //Assert
                Assert.IsTrue(result.IsOK);
                Assert.AreEqual(value, result.Data);
            }
        }        
    }
}
