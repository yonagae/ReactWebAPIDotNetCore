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
    public class UpdateTransactionHandlerTest
    {
        [TestMethod]
        public void Handle_ValidTransaction_ReturnsUpdatedTransaction()
        {
            ITransactionRepository transactionRepository = Substitute.For<ITransactionRepository>();
            UpdateTransactionHandler handler = new UpdateTransactionHandler(transactionRepository);    
            var transactionAmounts = new List<TransactionAmount>() { new TransactionAmount(0, 1, 10) };
            Transaction transaction = new Transaction(1, 1, new DateTime(2022, 01, 12), "Continente Gaia", "Continente", transactionAmounts, 11.0m);
            transactionRepository.UpdateAsync(transaction).ReturnsForAnyArgs<Transaction>(transaction);

            var result = handler.Handle(new UpdateTransactionCommand(transaction), new System.Threading.CancellationToken());

            transactionRepository.Received().UpdateAsync(Arg.Is<Transaction>(transaction));
            result.Result.Success.Should().BeTrue();
            result.Result.Messages.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void Handle_NonExistingTransaction_ReturnsFail()
        {
            ITransactionRepository transactionRepository = Substitute.For<ITransactionRepository>();
            UpdateTransactionHandler handler = new UpdateTransactionHandler(transactionRepository);
            var transactionAmounts = new List<TransactionAmount>() { new TransactionAmount(0, 1, 10) };
            Transaction transaction = new Transaction(1, 1, new DateTime(2022, 01, 12), "Continente Gaia", "Continente", transactionAmounts, 11.0m);
            transactionRepository.UpdateAsync(transaction).ReturnsForAnyArgs<Transaction>(i => null);


            var result = handler.Handle(new UpdateTransactionCommand(transaction), new System.Threading.CancellationToken());

            transactionRepository.Received().UpdateAsync(Arg.Is<Transaction>(transaction));
            result.Result.Success.Should().BeFalse();
            result.Result.Messages.Should().NotBeEmpty();
        }

        [TestMethod]
        public void Handle_InvalidTransaction_ReturnsFail()
        {
            ITransactionRepository transactionRepository = Substitute.For<ITransactionRepository>();
            UpdateTransactionHandler handler = new UpdateTransactionHandler(transactionRepository);
            User user = new User("Jonh Main");
            Transaction transaction = new Transaction(1, 1, new DateTime(2022, 01, 12), "Continente Gaia", "Continente", null, 11.0m);

            var result = handler.Handle(new UpdateTransactionCommand(transaction), new System.Threading.CancellationToken());

            transactionRepository.DidNotReceive().UpdateAsync(Arg.Is<Transaction>(transaction));
            result.Result.Success.Should().BeFalse();
            result.Result.Messages.Should().NotBeEmpty();  
        }
    }
}
