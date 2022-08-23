using FinBY.Domain.Data.PagedResult;
using FinBY.Domain.Entities;
using FinBY.Domain.Repositories;
using FinBY.Infra.Repository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.DBTests
{
    [TestClass]
    public class GetAllWithDetailsAsPagedResultAsyncTest
    {
        [ClassInitialize]
        public static async Task ClassSetup(TestContext context)
        {
            TestDatabaseFixture.Instance.Database.ExecuteSqlRaw("delete from dbo.[TransactionAmount] where 1 = 1");
            TestDatabaseFixture.Instance.Database.ExecuteSqlRaw("delete from dbo.[Transaction] where 1 = 1");

            IUnitOfWork unitOfWork = new UnitOfWork(TestDatabaseFixture.Instance);
            var transactionType = (await unitOfWork.TransactionTypeRepository.GetAllAsync()).FirstOrDefault();
            List<Transaction> transactions = new List<Transaction>();

            for (int i = 1; i <= 100; i++)
            {
                List<TransactionAmount> transactionAmounts = new List<TransactionAmount>()
                    {
                        new TransactionAmount(0, 1, 1.0m),
                        new TransactionAmount(0, 2, 2.0m)
                    };
                var transaction = new Transaction(
                    (i % 3) + 1
                    , (i % 2) + 1
                    , new DateTime(2022, 01, 01).AddDays(i)
                    , $"Gasto número {i}"
                    , $"Gasto {i}"
                    );
                transaction.AddAmounts(transactionAmounts);
                transactions.Add(transaction);
            }

            TestDatabaseFixture.Instance.AddRange(transactions);
            TestDatabaseFixture.Instance.SaveChanges();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TestDatabaseFixture.Instance.Database.ExecuteSqlRaw("delete from dbo.[TransactionAmount] where 1 = 1");
            TestDatabaseFixture.Instance.Database.ExecuteSqlRaw("delete from dbo.[Transaction] where 1 = 1");
        }

        private IUnitOfWork _unitOfWork;
        public GetAllWithDetailsAsPagedResultAsyncTest()
        {
            _unitOfWork = new UnitOfWork(TestDatabaseFixture.Instance);
        }

        [TestMethod]
        public async Task GetAllWithDetailsAsPagedResultAsync_FilterByDate_ReturnTransactionBetweenTheDates()
        {
            var tParams = new PagedTransactionParams()
            {
                PageSize = 50,
                PageNumber = 1,
                DataBegin = new DateTime(2022, 02, 01),
                DataEnd = new DateTime(2022, 02, 28),
            };
            var result = await _unitOfWork.TransactionRepository.GetAllWithDetailsAsPagedResultAsync(tParams);
            result.Results.Count.Should().Be(28);
            result.Results.Should().OnlyContain(x => x.Date >= new DateTime(2022, 02, 01));
            result.Results.Should().OnlyContain(x => x.Date <= new DateTime(2022, 02, 28));
        }

        [TestMethod]
        public async Task GetAllWithDetailsAsPagedResultAsync_FilterByDateWithA10PageSize_Return10TransactionBetweenTheDates()
        {
            var tParams = new PagedTransactionParams()
            {
                PageSize = 10,
                PageNumber = 1,
                DataBegin = new DateTime(2022, 02, 01),
                DataEnd = new DateTime(2022, 02, 28),
            };
            var result = await _unitOfWork.TransactionRepository.GetAllWithDetailsAsPagedResultAsync(tParams);
            result.Results.Count.Should().Be(10);
            result.Results.Should().OnlyContain(x => x.Date >= new DateTime(2022, 02, 01));
            result.Results.Should().OnlyContain(x => x.Date <= new DateTime(2022, 02, 28));
        }

        [TestMethod]
        public async Task GetAllWithDetailsAsPagedResultAsync_PageNumber2_ReturnTransactionFromPage2()
        {
             var tParams = new PagedTransactionParams()
            {
                PageSize = 10,
                PageNumber = 2,
            };
            var result = await _unitOfWork.TransactionRepository.GetAllWithDetailsAsPagedResultAsync(tParams);

            result.Results.Count.Should().Be(10);
            result.Results.Should().OnlyContain(x => x.Date > new DateTime(2022, 01, 11));
            result.Results.Should().OnlyContain(x => x.Date < new DateTime(2022, 01, 22));
        }

        [TestMethod]
        public async Task GetAllWithDetailsAsPagedResultAsync_FilterByUserId_ReturnTransactionCreatedOnlyByTheUserId()
        {
            var tParams = new PagedTransactionParams()
            {
                PageSize = 100,
                PageNumber = 1,
                UserId = 1
            };
            var result = await _unitOfWork.TransactionRepository.GetAllWithDetailsAsPagedResultAsync(tParams);
            result.Results.Count.Should().Be(50);
            result.Results.Should().OnlyContain(x => x.UserId == 1);
        }

        [TestMethod]
        public async Task GetAllWithDetailsAsPagedResultAsync_FilterByTransactionType_ReturnTransactionFilteredByTheTransactionType()
        {
            var tParams = new PagedTransactionParams()
            {
                PageSize = 50,
                PageNumber = 1,
                TransactionType = 2
            };
            var result = await _unitOfWork.TransactionRepository.GetAllWithDetailsAsPagedResultAsync(tParams);
            result.Results.Count.Should().Be(34);
            result.Results.Should().OnlyContain(x => x.TransactionType.Id == 2);
        }
    }
}
