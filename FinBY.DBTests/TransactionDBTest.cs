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
            TestDatabaseFixture.Instance.Database.ExecuteSqlRaw("delete from dbo.[TransactionAmount] where 1 = 1");
            TestDatabaseFixture.Instance.Database.ExecuteSqlRaw("delete from dbo.[Transaction] where 1 = 1");   
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

            var transactionList = await _unitOfWork.TransactionRepository.GetAllWithDetailsAsListAsync();
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
            Transaction transaction = await CreateValidTransaction(_unitOfWork);
            CreateTransactionHandler createTransactionHandler = new CreateTransactionHandler(_unitOfWork);
            await createTransactionHandler.Handle(new CreateTransactionCommand(transaction), new System.Threading.CancellationToken());

            transaction.AddAmount(new TransactionAmount(transaction.Id, 1, 11.11m));
            UpdateTransactionHandler updateTransactionHandler = new UpdateTransactionHandler(_unitOfWork);
            await updateTransactionHandler.Handle(new UpdateTransactionCommand(transaction), new System.Threading.CancellationToken());

            var transactionList = await _unitOfWork.TransactionRepository.GetAllWithDetailsAsListAsync();
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
            Transaction transaction = await CreateValidTransaction(_unitOfWork);
            CreateTransactionHandler createTransactionHandler = new CreateTransactionHandler(_unitOfWork);
            var resultTra = await createTransactionHandler.Handle(new CreateTransactionCommand(transaction), new System.Threading.CancellationToken());

            DeleteTransactionHandler deleteTransaction = new DeleteTransactionHandler(_unitOfWork);
            var result = await deleteTransaction.Handle(new DeleteTransactionCommand(((Transaction)resultTra.Data).Id), new System.Threading.CancellationToken());

            var transactionList = await _unitOfWork.TransactionRepository.GetAllWithDetailsAsListAsync();
            transactionList.Count.Should().Be(0);
            (await _unitOfWork.TransactionRepository.GetAllAsync()).Count.Should().Be(0);
            (await _unitOfWork.TransactionAmountRepository.GetAllAsync()).Count.Should().Be(0);
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