using FinBY.Domain.Commands;
using FinBY.Domain.Entities;
using FinBY.Domain.Handler;
using FinBY.Domain.Repositories;
using FinBY.Infra.Repository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinBY.DBTests
{
    [TestClass]
    public class TransactionDBTest
    {
        [TestCleanup]
        public void CleanTables()
        {
            //TestDatabaseFixture.Instance.Database.ExecuteSqlRaw("delete from dbo.[TransactionAmount] where 1 = 1");
            //TestDatabaseFixture.Instance.Database.ExecuteSqlRaw("delete from dbo.[Transaction] where 1 = 1");   
        }

        private IUnitOfWork _unitOfWork;
        public TransactionDBTest()
        {
            _unitOfWork = new UnitOfWork(TestDatabaseFixture.Instance);
        }

        [TestMethod]
        public async Task CreateTransaction()
        {
            Transaction transaction = await CreateValidTransaction(_unitOfWork);

            CreateTransactionHandler createTransactionHandler = new CreateTransactionHandler(_unitOfWork);
            await createTransactionHandler.Handle(new CreateTransactionCommand(transaction), new System.Threading.CancellationToken());

            var createdTransaction = await _unitOfWork.TransactionRepository.GetDetailedByIdAsync(transaction.Id);
            createdTransaction.Id.Should().BeGreaterThan(0);
            createdTransaction.TotalAmount.Should().Be(30.85m);
            createdTransaction.Description.Should().Be("Gasto 00");
            createdTransaction.ShortDescription.Should().Be("Gasto 0");
            createdTransaction.Date.Year.Should().Be(2022);
            createdTransaction.Date.Month.Should().Be(02);
            createdTransaction.Date.Day.Should().Be(11);
            createdTransaction.TransactionAmounts.Count.Should().Be(2);
            createdTransaction.TransactionAmounts.Should().OnlyContain(x => x.TransactionId == createdTransaction.Id);
            createdTransaction.TransactionAmounts.Should().Contain(x => x.UserId == 1 && x.Amount == 10.55m);
            createdTransaction.TransactionAmounts.Should().Contain(x => x.UserId == 2 && x.Amount == 20.3m);
        }

        [TestMethod]
        public async Task UpdateTransaction()
        {
            Transaction transaction = await CreateValidTransaction(_unitOfWork);
            CreateTransactionHandler createTransactionHandler = new CreateTransactionHandler(_unitOfWork);
            await createTransactionHandler.Handle(new CreateTransactionCommand(transaction), new System.Threading.CancellationToken());

            transaction.AddAmount(new TransactionAmount(transaction.Id, 1, 11.11m));
            UpdateTransactionHandler updateTransactionHandler = new UpdateTransactionHandler(_unitOfWork);
            await updateTransactionHandler.Handle(new UpdateTransactionCommand(transaction), new System.Threading.CancellationToken());

            var createdTransaction = await _unitOfWork.TransactionRepository.GetDetailedByIdAsync(transaction.Id);
            createdTransaction.Id.Should().BeGreaterThan(0);
            createdTransaction.TotalAmount.Should().Be(41.96m);
            createdTransaction.Description.Should().Be("Gasto 00");
            createdTransaction.ShortDescription.Should().Be("Gasto 0");
            createdTransaction.Date.Year.Should().Be(2022);
            createdTransaction.Date.Month.Should().Be(02);
            createdTransaction.Date.Day.Should().Be(11);
            createdTransaction.TransactionAmounts.Count.Should().Be(3);
            createdTransaction.TransactionAmounts.Should().OnlyContain(x => x.TransactionId == createdTransaction.Id);
            createdTransaction.TransactionAmounts.Should().Contain(x => x.UserId == 1 && x.Amount == 11.11m);
        }


        [TestMethod]
        public async Task DeleteTransaction()
        {
            Transaction transaction = await CreateValidTransaction(_unitOfWork);
            CreateTransactionHandler createTransactionHandler = new CreateTransactionHandler(_unitOfWork);
            var resultTra = await createTransactionHandler.Handle(new CreateTransactionCommand(transaction), new System.Threading.CancellationToken());

            DeleteTransactionHandler deleteTransaction = new DeleteTransactionHandler(_unitOfWork);
            var result = await deleteTransaction.Handle(new DeleteTransactionCommand(((Transaction)resultTra.Data).Id), new System.Threading.CancellationToken());

            var createdTransaction = await _unitOfWork.TransactionRepository.GetDetailedByIdAsync(((Transaction)resultTra.Data).Id);
            createdTransaction.Should().Be(null);
        }


        private static async Task<Transaction> CreateValidTransaction(IUnitOfWork unitOfWork)
        {
            var transactionType = (await unitOfWork.TransactionTypeRepository.GetAllAsync()).FirstOrDefault();
            List<TransactionAmount> transactionAmounts = new List<TransactionAmount>()
            {
                new TransactionAmount(0, 1, 10.55m),
                new TransactionAmount(0, 2, 20.3m)
            };
            var transaction = new Transaction(transactionType.Id, 1, new DateTime(2022, 02, 11), "Gasto 00", "Gasto 0");
            transaction.AddAmounts(transactionAmounts);
            return transaction;
        }
    }
}