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
    public class DeleteTransactionHandlerTest
    {
        private readonly int TRANSACTION_ID = 999;

        [TestMethod]
        public void Handle_NonExistingTransaction_Fails()
        {
            IUnitOfWork unitOfWork = new FakeUnitOfWork();
            DeleteTransactionHandler handler = new DeleteTransactionHandler(unitOfWork);
            unitOfWork.TransactionRepository.GetByIdAsync(TRANSACTION_ID).Returns<Transaction>(i => null);

            var result = handler.Handle(new DeleteTransactionCommand(TRANSACTION_ID), new System.Threading.CancellationToken());

            unitOfWork.TransactionRepository.DidNotReceive().Remove(TRANSACTION_ID);
            result.Result.Success.Should().BeFalse();
            result.Result.Messages.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void Handle_ValidTransaction_ReturnsCreatedTransaction()
        {
            IUnitOfWork unitOfWork = new FakeUnitOfWork();
            DeleteTransactionHandler handler = new DeleteTransactionHandler(unitOfWork);
            unitOfWork.TransactionRepository.GetByIdAsync(TRANSACTION_ID).Returns<Transaction>(new Transaction(1, 1, new DateTime(), "Descrition", "ShortDescription"));

            var result = handler.Handle(new DeleteTransactionCommand(TRANSACTION_ID), new System.Threading.CancellationToken());

            unitOfWork.TransactionRepository.Received().Remove(TRANSACTION_ID);
            result.Result.Success.Should().BeTrue();
            result.Result.Messages.Should().BeNullOrEmpty();
        }
    }
}
