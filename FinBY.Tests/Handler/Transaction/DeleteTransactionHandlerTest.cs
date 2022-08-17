using FinBY.Domain.Commands;
using FinBY.Domain.Entities;
using FinBY.Domain.Handler;
using FinBY.Domain.Repositories;
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
            ITransactionRepository transactionRepository = Substitute.For<ITransactionRepository>();
            DeleteTransactionHandler handler = new DeleteTransactionHandler(transactionRepository);
            transactionRepository.GetByIdAsync(TRANSACTION_ID).Returns<Transaction>(i => null);

            var result = handler.Handle(new DeleteTransactionCommand(TRANSACTION_ID), new System.Threading.CancellationToken());

            transactionRepository.Received().DeleteAsync(TRANSACTION_ID);
            result.Result.Success.Should().BeFalse();
            result.Result.Messages.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void Handle_ValidTransaction_ReturnsCreatedTransaction()
        {
            ITransactionRepository transactionRepository = Substitute.For<ITransactionRepository>();
            DeleteTransactionHandler handler = new DeleteTransactionHandler(transactionRepository);
            transactionRepository.GetByIdAsync(TRANSACTION_ID).Returns<Transaction>(new Transaction());

            var result = handler.Handle(new DeleteTransactionCommand(TRANSACTION_ID), new System.Threading.CancellationToken());

            transactionRepository.Received().DeleteAsync(TRANSACTION_ID);
            result.Result.Success.Should().BeFalse();
            result.Result.Messages.Should().NotBeNullOrEmpty();
        }
    }
}
