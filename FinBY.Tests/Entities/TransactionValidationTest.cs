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
    public class TransactionValidationTest
    {
        private User _user;
        private List<TransactionAmount> _transactionAmounts;

        public TransactionValidationTest()
        {
            _transactionAmounts = new List<TransactionAmount>() { new TransactionAmount(0, 1, 10) };
        }

        [TestMethod]   
        public void Validate_BeAValidTransaction_ReturnIsValidTrue()
        {
            Transaction transaction = new Transaction(1, 1, new DateTime(2022, 01, 12), "Continente Gaia", "Continente", _transactionAmounts);

            var validationResult = transaction.Validate();

            validationResult.isValid.Should().BeTrue();
            validationResult.errorMessages.Should().BeEmpty();
        }

        [TestMethod]
        public void Validate_TransactionAmountNull_ReturnIsValidFalse()
        {
            Transaction transaction = new Transaction(1, 1, new DateTime(2022, 01, 12), "Continente Gaia", "Continente", null);

            var validationResult = transaction.Validate();

            validationResult.isValid.Should().BeFalse();
            validationResult.errorMessages.Should().NotBeEmpty();
            validationResult.errorMessages.Count.Should().Be(1);
        }

        [TestMethod]
        public void Validate_TransactionAmountEmpty_ReturnIsValidFalse()
        {
            Transaction transaction = new Transaction(1, 1, new DateTime(2022, 01, 12), "Continente Gaia", "Continente", new List<TransactionAmount>());

            var validationResult = transaction.Validate();

            validationResult.isValid.Should().BeFalse();
            validationResult.errorMessages.Should().NotBeEmpty();
            validationResult.errorMessages.Count.Should().Be(1);
        }
      
        [TestMethod]
        public void Validate_NullDescription_ReturnIsValidFalse()
        {
            Transaction transaction = new Transaction(1, 1, new DateTime(2022, 01, 12), null, "Continente", _transactionAmounts);

            var validationResult = transaction.Validate();

            validationResult.isValid.Should().BeFalse();
            validationResult.errorMessages.Should().NotBeEmpty();
            validationResult.errorMessages.Count.Should().Be(1);
        }

        [TestMethod]
        public void Validate_InvalidDescription_ReturnIsValidFalse()
        {
            string invalidDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec sagittis posuere lacus eu rhoncus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum non sem vitae mauris vehicula cursus. Pellentesque volutpat turpis quam, in luctus ante dignissim at. Sed efficitur, sapien eu sollicitudin mattis, augue est ornare lectus, in malesuada elit ligula ut felis. Nunc tincidunt tincidunt lobortis. Maecenas ut purus rutrum, scelerisque ante eu, euismod neque. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Nullam ac tristique nulla. Etiam ex ipsum, eleifend id ultrices at, pharetra quis orci.";
            Transaction transaction = new Transaction(1, 1, new DateTime(2022, 01, 12), invalidDescription, "Continente", _transactionAmounts);

            var validationResult = transaction.Validate();

            validationResult.isValid.Should().BeFalse();
            validationResult.errorMessages.Should().NotBeEmpty();
            validationResult.errorMessages.Count.Should().Be(1);
        }

        [TestMethod]
        public void Validate_NullShortDescription_ReturnIsValidFalse()
        {
            Transaction transaction = new Transaction(1, 1, new DateTime(2022, 01, 12), "Continente", null, _transactionAmounts);

            var validationResult = transaction.Validate();

            validationResult.isValid.Should().BeFalse();
            validationResult.errorMessages.Should().NotBeEmpty();
            validationResult.errorMessages.Count.Should().Be(1);
        }

        [TestMethod]
        public void Validate_InvalidShortDescription_ReturnIsValidFalse()
        {
            string invalidDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec sagittis posuere lacus eu rhoncus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum non sem vitae mauris vehicula cursus. Pellentesque volutpat turpis quam, in luctus ante dignissim at. Sed efficitur, sapien eu sollicitudin mattis, augue est ornare lectus, in malesuada elit ligula ut felis. Nunc tincidunt tincidunt lobortis. Maecenas ut purus rutrum, scelerisque ante eu, euismod neque. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Nullam ac tristique nulla. Etiam ex ipsum, eleifend id ultrices at, pharetra quis orci.";
            Transaction transaction = new Transaction(1, 1, new DateTime(2022, 01, 12), "Continente", invalidDescription, _transactionAmounts
                );

            var validationResult = transaction.Validate();

            validationResult.isValid.Should().BeFalse();
            validationResult.errorMessages.Should().NotBeEmpty();
            validationResult.errorMessages.Count.Should().Be(1);
        }

        [TestMethod]
        public void Validate_InvalidShortDescriptionAndDescription_ReturnIsValidFalse()
        {
            string invalidDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec sagittis posuere lacus eu rhoncus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum non sem vitae mauris vehicula cursus. Pellentesque volutpat turpis quam, in luctus ante dignissim at. Sed efficitur, sapien eu sollicitudin mattis, augue est ornare lectus, in malesuada elit ligula ut felis. Nunc tincidunt tincidunt lobortis. Maecenas ut purus rutrum, scelerisque ante eu, euismod neque. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Nullam ac tristique nulla. Etiam ex ipsum, eleifend id ultrices at, pharetra quis orci.";
            Transaction transaction = new Transaction(1, 1, new DateTime(2022, 01, 12), invalidDescription, invalidDescription, _transactionAmounts);

            var validationResult = transaction.Validate();

            validationResult.isValid.Should().BeFalse();
            validationResult.errorMessages.Should().NotBeEmpty();
            validationResult.errorMessages.Count.Should().Be(2);
        }

        [TestMethod]
        public void Validate_InvalidShortDescriptionAndDescriptionWithNullTransactionAmounts_ReturnIsValidFalse()
        {
            string invalidDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec sagittis posuere lacus eu rhoncus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum non sem vitae mauris vehicula cursus. Pellentesque volutpat turpis quam, in luctus ante dignissim at. Sed efficitur, sapien eu sollicitudin mattis, augue est ornare lectus, in malesuada elit ligula ut felis. Nunc tincidunt tincidunt lobortis. Maecenas ut purus rutrum, scelerisque ante eu, euismod neque. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Nullam ac tristique nulla. Etiam ex ipsum, eleifend id ultrices at, pharetra quis orci.";
            Transaction transaction = new Transaction(0, 1, new DateTime(2022, 01, 12), invalidDescription, invalidDescription, null);

            var validationResult = transaction.Validate();

            validationResult.isValid.Should().BeFalse();
            validationResult.errorMessages.Should().NotBeEmpty();
            validationResult.errorMessages.Count.Should().Be(3);
        }

    }
}
