using FinBY.Domain.Commands;
using FinBY.Domain.Entities;
using FinBY.Domain.Handler;
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
            TestDatabaseFixture.Instance.Database.ExecuteSqlRaw("delete from dbo.[TransactionAmount] where 1 = 1");
            TestDatabaseFixture.Instance.Database.ExecuteSqlRaw("delete from dbo.[Transaction] where 1 = 1");   
        }

        [TestMethod]
        public async Task CreateTransaction()
        {
            UnitOfWork unitOfWork = new UnitOfWork(TestDatabaseFixture.Instance);
            Transaction transaction = await CreateValidTransaction(unitOfWork);

            CreateTransactionHandler createTransactionHandler = new CreateTransactionHandler(unitOfWork);
            await createTransactionHandler.Handle(new CreateTransactionCommand(transaction), new System.Threading.CancellationToken());

            var transactionList = await unitOfWork.TransactionRepository.GetAllWithDetailsAsListAsync();
            transactionList.Count.Should().Be(1);
            transactionList[0].Id.Should().BeGreaterThan(0);
            transactionList[0].TotalAmount.Should().Be(30.85m);
            transactionList[0].Description.Should().Be("Gasto 00");
            transactionList[0].ShortDescription.Should().Be("Gasto 0");
            transactionList[0].Date.Year.Should().Be(2022);
            transactionList[0].Date.Month.Should().Be(02);
            transactionList[0].Date.Day.Should().Be(11);
            transactionList[0].TransactionAmounts.Count.Should().Be(2);
            transactionList[0].TransactionAmounts.Should().OnlyContain(x => x.TransactionId == transactionList[0].Id);
            transactionList[0].TransactionAmounts.Should().Contain(x => x.UserId == 1 && x.Amount == 10.55m);
            transactionList[0].TransactionAmounts.Should().Contain(x => x.UserId == 2 && x.Amount == 20.3m);
        }

        [TestMethod]
        public async Task UpdateTransaction()
        {
            UnitOfWork unitOfWork = new UnitOfWork(TestDatabaseFixture.Instance);
            Transaction transaction = await CreateValidTransaction(unitOfWork);
            CreateTransactionHandler createTransactionHandler = new CreateTransactionHandler(unitOfWork);
            await createTransactionHandler.Handle(new CreateTransactionCommand(transaction), new System.Threading.CancellationToken());

            transaction.AddTransactionAmount(new TransactionAmount(transaction.Id, 1, 11.11m));
            UpdateTransactionHandler updateTransactionHandler = new UpdateTransactionHandler(unitOfWork);
            await updateTransactionHandler.Handle(new UpdateTransactionCommand(transaction), new System.Threading.CancellationToken());

            var transactionList = await unitOfWork.TransactionRepository.GetAllWithDetailsAsListAsync();
            transactionList.Count.Should().Be(1);
            transactionList[0].Id.Should().BeGreaterThan(0);
            transactionList[0].TotalAmount.Should().Be(41.96m);
            transactionList[0].Description.Should().Be("Gasto 00");
            transactionList[0].ShortDescription.Should().Be("Gasto 0");
            transactionList[0].Date.Year.Should().Be(2022);
            transactionList[0].Date.Month.Should().Be(02);
            transactionList[0].Date.Day.Should().Be(11);
            transactionList[0].TransactionAmounts.Count.Should().Be(3);
            transactionList[0].TransactionAmounts.Should().OnlyContain(x => x.TransactionId == transactionList[0].Id);
            transactionList[0].TransactionAmounts.Should().Contain(x => x.UserId == 1 && x.Amount == 10.55m);
            transactionList[0].TransactionAmounts.Should().Contain(x => x.UserId == 2 && x.Amount == 20.3m);
            transactionList[0].TransactionAmounts.Should().Contain(x => x.UserId == 1 && x.Amount == 11.11m);
        }


        [TestMethod]
        public async Task DeleteTransaction()
        {
            UnitOfWork unitOfWork = new UnitOfWork(TestDatabaseFixture.Instance);
            Transaction transaction = await CreateValidTransaction(unitOfWork);
            CreateTransactionHandler createTransactionHandler = new CreateTransactionHandler(unitOfWork);
            var resultTra = await createTransactionHandler.Handle(new CreateTransactionCommand(transaction), new System.Threading.CancellationToken());

            DeleteTransactionHandler deleteTransaction = new DeleteTransactionHandler(unitOfWork);
            var result = await deleteTransaction.Handle(new DeleteTransactionCommand(((Transaction)resultTra.Data).Id), new System.Threading.CancellationToken());

            var transactionList = await unitOfWork.TransactionRepository.GetAllWithDetailsAsListAsync();
            transactionList.Count.Should().Be(0);
            (await unitOfWork.TransactionRepository.GetAllAsync()).Count.Should().Be(0);
            (await unitOfWork.TransactionAmountRepository.GetAllAsync()).Count.Should().Be(0);
        }


        private static async Task<Transaction> CreateValidTransaction(UnitOfWork unitOfWork)
        {
            var transactionType = (await unitOfWork.TransactionTypeRepository.GetAllAsync()).FirstOrDefault();
            List<TransactionAmount> transactionAmounts = new List<TransactionAmount>()
            {
                new TransactionAmount(0, 1, 10.55m),
                new TransactionAmount(0, 2, 20.3m)
            };
            return new Transaction(transactionType.Id, 1, new DateTime(2022, 02, 11), "Gasto 00", "Gasto 0", transactionAmounts);
        }
    }
}