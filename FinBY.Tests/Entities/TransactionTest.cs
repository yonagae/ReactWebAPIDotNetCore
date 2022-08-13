using FinBY.Domain.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Tests.Entities
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void AddTransactionAmount_NullValue_ReturnFalseDontChangeTotalAmount()
        {
            Transaction transaction = new Transaction();
            transaction.TotalAmount.Should().Be(0);
            transaction.TransactionAmounts.Count.Should().Be(0);

            transaction.AddTransactionAmount(null).Should().BeFalse();
            transaction.TotalAmount.Should().Be(0);
            transaction.TransactionAmounts.Count.Should().Be(0);
         }

        [TestMethod]
        public void AddTransactionAmount_PositiveValues_GetCorrectTotalAmount()
        {
            Transaction transaction = new Transaction();
            transaction.TotalAmount.Should().Be(0);
            transaction.TransactionAmounts.Count.Should().Be(0);

            transaction.AddTransactionAmount(new TransactionAmount(1, 11, 10.02m)).Should().BeTrue();
            transaction.TotalAmount.Should().Be(10.02m);
            transaction.TransactionAmounts.Count.Should().Be(1);

            transaction.AddTransactionAmount(new TransactionAmount(2, 11, 100.08m)).Should().BeTrue();
            transaction.TotalAmount.Should().Be(110.10m);
            transaction.TransactionAmounts.Count.Should().Be(2);

            transaction.AddTransactionAmount(new TransactionAmount(3, 11, 11)).Should().BeTrue();
            transaction.TotalAmount.Should().Be(121.10m);
            transaction.TransactionAmounts.Count.Should().Be(3);
        }

        [TestMethod]
        public void AddTransactionAmount_NegativeValues_GetCorrectTotalAmount()
        {
            Transaction transaction = new Transaction();
            transaction.TotalAmount.Should().Be(0);
            transaction.TransactionAmounts.Count.Should().Be(0);

            transaction.AddTransactionAmount(new TransactionAmount(1, 11, -10.02m)).Should().BeTrue();
            transaction.TotalAmount.Should().Be(-10.02m);
            transaction.TransactionAmounts.Count.Should().Be(1);

            transaction.AddTransactionAmount(new TransactionAmount(2, 11, -100.08m)).Should().BeTrue();
            transaction.TotalAmount.Should().Be(-110.10m);
            transaction.TransactionAmounts.Count.Should().Be(2);

            transaction.AddTransactionAmount(new TransactionAmount(3, 11, -11)).Should().BeTrue();
            transaction.TotalAmount.Should().Be(-121.10m);
            transaction.TransactionAmounts.Count.Should().Be(3);
        }
    }
}
