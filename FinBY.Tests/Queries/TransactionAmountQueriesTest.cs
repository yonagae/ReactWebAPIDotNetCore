using FinBY.Domain.Entities;
using FinBY.Domain.Queries;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Tests.Queries
{
    [TestClass]
    public  class TransactionAmountQueriesTest
    {
        private IList<TransactionAmount> _transactions;

        public TransactionAmountQueriesTest()
        {
            _transactions = new List<TransactionAmount>();
            _transactions.Add(new TransactionAmount(1, 1, 10.02m));
            _transactions.Add(new TransactionAmount(1, 2, 11.02m));
            _transactions.Add(new TransactionAmount(2, 3, 12.02m));
            _transactions.Add(new TransactionAmount(3, 4, 13.02m));
            _transactions.Add(new TransactionAmount(4, 5, 14.02m));
            _transactions.Add(new TransactionAmount(4, 6, 15.02m));
            _transactions.Add(new TransactionAmount(4, 7, 11.02m));
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void GetByTransactionId_Existing2TransactionsAmount_CorrectValues()
        {
            var result = _transactions.AsQueryable().Where(TransactionAmountQueries.GetByTransactionId(1));
            
            result.Count().Should().Be(2);
            result.Should().Contain(_transactions[1]);
            result.Should().Contain(_transactions[0]);
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void GetByTransactionId_Existing1TransactionsAmount_CorrectValues()
        {
            var result = _transactions.AsQueryable().Where(TransactionAmountQueries.GetByTransactionId(3));

            result.Count().Should().Be(1);
            result.Should().Contain(_transactions[3]);
        }
    }
}
