using FinBY.Domain.Commands;
using FinBY.Domain.Entities;
using FinBY.Domain.Handler;
using FinBY.Domain.Repositories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;

namespace FinBY.Tests.Handler
{
    [TestClass]
    public class CreateTransactionHandlerTest
    {
        [TestMethod]
        public void Handle_ValidTransaction_ReturnsCreatedTransaction()
        {
            ITransactionRepository transactionRepository = Substitute.For<ITransactionRepository>();
            CreateTransactionHandler handler = new CreateTransactionHandler(transactionRepository);            
            var transactionAmounts = new List<TransactionAmount>() { new TransactionAmount(0, 1, 10) };
            Transaction transaction = new Transaction(new TransactionType("Mercado"), 1, "Continente Gaia", "Continente", transactionAmounts, 11.0m);

            var result = handler.Handle(new CreateTransactionCommand(transaction), new System.Threading.CancellationToken());

            transactionRepository.Received().InsertAsync(Arg.Is<Transaction>(transaction));
            result.Result.Success.Should().BeTrue();
            result.Result.Messages.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void Handle_InvalidTransaction_ReturnsCreatedTransaction()
        {
            ITransactionRepository transactionRepository = Substitute.For<ITransactionRepository>();
            CreateTransactionHandler handler = new CreateTransactionHandler(transactionRepository);
            Transaction transaction = new Transaction(new TransactionType("Mercado"), 1, "Continente Gaia", "Continente", null, 11.0m);

            var result = handler.Handle(new CreateTransactionCommand(transaction), new System.Threading.CancellationToken());

            transactionRepository.DidNotReceive().InsertAsync(Arg.Is<Transaction>(transaction));
            result.Result.Success.Should().BeFalse();
            result.Result.Messages.Should().NotBeEmpty();  
        }
    }
}
