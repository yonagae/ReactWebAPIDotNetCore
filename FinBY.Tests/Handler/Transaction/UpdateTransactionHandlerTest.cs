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
using System.Threading.Tasks;

namespace FinBY.Tests.Handler
{
    [TestClass]
    public class UpdateTransactionHandlerTest
    {
        [TestMethod]
        public async Task Handle_ValidTransaction_ReturnsUpdatedTransaction()
        {
            IUnitOfWork unitOfWork = new FakeUnitOfWork();
            UpdateTransactionHandler handler = new UpdateTransactionHandler(unitOfWork);    
            var transactionAmounts = new List<TransactionAmount>() { new TransactionAmount(0, 1, 10) };
            Transaction transaction = new Transaction(1, 1, new DateTime(2022, 01, 12), "Continente Gaia", "Continente");
            transaction.AddAmounts(transactionAmounts);
            unitOfWork.TransactionRepository.GetByIdAsync(transaction.Id).Returns<Transaction>(transaction);

            var result = handler.Handle(new UpdateTransactionCommand(transaction), new System.Threading.CancellationToken());

            unitOfWork.TransactionRepository.Received().UpdateTransactionWithAmounts(Arg.Is<Transaction>(transaction));
            result.Result.Success.Should().BeTrue();
            result.Result.Messages.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void Handle_NonExistingTransaction_ReturnsFail()
        {
            IUnitOfWork unitOfWork = new FakeUnitOfWork();
            UpdateTransactionHandler handler = new UpdateTransactionHandler(unitOfWork);
            var transactionAmounts = new List<TransactionAmount>() { new TransactionAmount(0, 1, 10) };
            Transaction transaction = new Transaction(1, 1, new DateTime(2022, 01, 12), "Continente Gaia", "Continente");
            transaction.AddAmounts(transactionAmounts);


            var result = handler.Handle(new UpdateTransactionCommand(transaction), new System.Threading.CancellationToken());

            unitOfWork.TransactionRepository.DidNotReceive().Update(Arg.Is<Transaction>(transaction));
            result.Result.Success.Should().BeFalse();
            result.Result.Messages.Should().NotBeEmpty();
        }

        [TestMethod]
        public void Handle_InvalidTransaction_ReturnsFail()
        {
            IUnitOfWork unitOfWork = new FakeUnitOfWork();
            UpdateTransactionHandler handler = new UpdateTransactionHandler(unitOfWork);
            User user = new User("Jonh Main");
            Transaction transaction = new Transaction(1, 1, new DateTime(2022, 01, 12), "Continente Gaia", "Continente");

            var result = handler.Handle(new UpdateTransactionCommand(transaction), new System.Threading.CancellationToken());

            unitOfWork.TransactionRepository.DidNotReceive().Update(Arg.Is<Transaction>(transaction));
            result.Result.Success.Should().BeFalse();
            result.Result.Messages.Should().NotBeEmpty();  
        }
    }
}
