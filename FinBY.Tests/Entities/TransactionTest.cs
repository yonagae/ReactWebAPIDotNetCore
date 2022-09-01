using FinBY.Domain.Entities;
using FinBY.Domain.Enum;
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
            Transaction transaction = new Transaction(eTransactionFlow.Credit, 1, 1, new DateTime(), "Descrition", "ShortDescription");
            transaction.TotalAmount.Should().Be(0);
            transaction.TransactionAmounts.Count.Should().Be(0);

            transaction.AddAmount(null).Should().BeFalse();
            transaction.TotalAmount.Should().Be(0);
            transaction.TransactionAmounts.Count.Should().Be(0);
         }

        [TestMethod]
        public void AddTransactionAmount_PositiveValues_GetCorrectTotalAmount()
        {
            Transaction transaction = new Transaction(eTransactionFlow.Credit, 1, 1, new DateTime(), "Descrition", "ShortDescription");
            transaction.TotalAmount.Should().Be(0);
            transaction.TransactionAmounts.Count.Should().Be(0);

            transaction.AddAmount(new TransactionAmount(1, 11, 10.02m)).Should().BeTrue();
            transaction.TotalAmount.Should().Be(10.02m);
            transaction.TransactionAmounts.Count.Should().Be(1);

            transaction.AddAmount(new TransactionAmount(2, 11, 100.08m)).Should().BeTrue();
            transaction.TotalAmount.Should().Be(110.10m);
            transaction.TransactionAmounts.Count.Should().Be(2);

            transaction.AddAmount(new TransactionAmount(3, 11, 11)).Should().BeTrue();
            transaction.TotalAmount.Should().Be(121.10m);
            transaction.TransactionAmounts.Count.Should().Be(3);
        }

        [TestMethod]
        public void AddTransactionAmount_NegativeValues_GetCorrectTotalAmount()
        {
            Transaction transaction = new Transaction(eTransactionFlow.Credit, 1, 1, new DateTime(), "Descrition", "ShortDescription");
            transaction.TotalAmount.Should().Be(0);
            transaction.TransactionAmounts.Count.Should().Be(0);

            transaction.AddAmount(new TransactionAmount(1, 11, -10.02m)).Should().BeTrue();
            transaction.TotalAmount.Should().Be(-10.02m);
            transaction.TransactionAmounts.Count.Should().Be(1);

            transaction.AddAmount(new TransactionAmount(2, 11, -100.08m)).Should().BeTrue();
            transaction.TotalAmount.Should().Be(-110.10m);
            transaction.TransactionAmounts.Count.Should().Be(2);

            transaction.AddAmount(new TransactionAmount(3, 11, -11)).Should().BeTrue();
            transaction.TotalAmount.Should().Be(-121.10m);
            transaction.TransactionAmounts.Count.Should().Be(3);
        }

        [TestMethod]
        public void RemoveTransactionAmount_NullValue_ReturnFalseDontChangeTotalAmount()
        {
            Transaction transaction = new Transaction(eTransactionFlow.Credit, 1, 1, new DateTime(), "Descrition", "ShortDescription");
            transaction.TotalAmount.Should().Be(0);
            transaction.TransactionAmounts.Count.Should().Be(0);

            transaction.RemoveTransactionAmount(null).Should().BeFalse();
            transaction.TotalAmount.Should().Be(0);
            transaction.TransactionAmounts.Count.Should().Be(0);
        }

        [TestMethod]
        public void RemoveTransactionAmount_PositiveValues_GetCorrectTotalAmount()
        {
            Transaction transaction = new Transaction(eTransactionFlow.Credit, 1, 1, new DateTime(), "Descrition", "ShortDescription");
            var t1 = new TransactionAmount(transaction.Id, 11, 10.02m);
            var t2 = new TransactionAmount(transaction.Id, 11, 100.08m);
            var t3 = new TransactionAmount(transaction.Id, 11, 11);
            transaction.AddAmount(t1);
            transaction.AddAmount(t2);
            transaction.AddAmount(t3);

            transaction.RemoveTransactionAmount(t3).Should().BeTrue();
            transaction.TotalAmount.Should().Be(110.1m);
            transaction.TransactionAmounts.Count.Should().Be(2);

            transaction.RemoveTransactionAmount(t2).Should().BeTrue();
            transaction.TotalAmount.Should().Be(10.02m);
            transaction.TransactionAmounts.Count.Should().Be(1);

            transaction.RemoveTransactionAmount(t1).Should().BeTrue();
            transaction.TotalAmount.Should().Be(0);
            transaction.TransactionAmounts.Count.Should().Be(0);
        }

        [TestMethod]
        public void RemoveTransactionAmount_NegativeValues_GetCorrectTotalAmount()
        {
            Transaction transaction = new Transaction(eTransactionFlow.Credit, 1, 1, new DateTime(), "Descrition", "ShortDescription");
            var t1 = new TransactionAmount(transaction.Id, 11, -10.02m);
            var t2 = new TransactionAmount(transaction.Id, 11, -100.08m);
            var t3 = new TransactionAmount(transaction.Id, 11, -11);
            transaction.AddAmount(t1);
            transaction.AddAmount(t2);
            transaction.AddAmount(t3);

            transaction.RemoveTransactionAmount(t3).Should().BeTrue();
            transaction.TotalAmount.Should().Be(-110.1m);
            transaction.TransactionAmounts.Count.Should().Be(2);

            transaction.RemoveTransactionAmount(t2).Should().BeTrue();
            transaction.TotalAmount.Should().Be(-10.02m);
            transaction.TransactionAmounts.Count.Should().Be(1);

            transaction.RemoveTransactionAmount(t1).Should().BeTrue();
            transaction.TotalAmount.Should().Be(0);
            transaction.TransactionAmounts.Count.Should().Be(0);
        }
    }
}
