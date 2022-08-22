using FinBY.Domain.Commands;
using FinBY.Domain.Entities;
using FinBY.Domain.Handler;
using FinBY.Domain.Repositories;
using FinBY.Tests.Controllers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;

namespace FinBY.Tests.Handler
{
    [TestClass]
    public class CreateTransactionHandlerTest
    {
        [TestMethod]
        public void Handle_ValidTransaction_ReturnsCreatedTransaction()
        {
            IUnitOfWork unitOfWork = new FakeUnitOfWork();
            CreateTransactionHandler handler = new CreateTransactionHandler(unitOfWork);            
            var transactionAmounts = new List<TransactionAmount>() { new TransactionAmount(0, 1, 10) };
            Transaction transaction = new Transaction(1, 1, new DateTime(2022, 01, 12), "Continente Gaia", "Continente", transactionAmounts);

            var result = handler.Handle(new CreateTransactionCommand(transaction), new System.Threading.CancellationToken());

            unitOfWork.TransactionRepository.Received().Add(Arg.Is<Transaction>(transaction));
            result.Result.Success.Should().BeTrue();
            result.Result.Messages.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void Handle_InvalidTransaction_ReturnsCreatedTransaction()
        {
            IUnitOfWork unitOfWork = new FakeUnitOfWork();
            CreateTransactionHandler handler = new CreateTransactionHandler(unitOfWork);
            Transaction transaction = new Transaction(1, 1, new DateTime(2022, 01, 12), "Continente Gaia", "Continente", null);

            var result = handler.Handle(new CreateTransactionCommand(transaction), new System.Threading.CancellationToken());

            unitOfWork.TransactionRepository.DidNotReceive().Add(Arg.Is<Transaction>(transaction));
            result.Result.Success.Should().BeFalse();
            result.Result.Messages.Should().NotBeEmpty();  
        }
    }
}
