using FinBY.Domain.Repositories;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Tests.Controllers
{
    public class FakeUnitOfWork  : IUnitOfWork
    {
        private ITransactionRepository _transactionRepository;
        private ITransactionTypeRepository _transactionTypeRepository;
        private ITransactionAmountRepository _transactionAmountRepository;

        public FakeUnitOfWork ()
        {
            _transactionRepository = Substitute.For<ITransactionRepository>();
            _transactionTypeRepository = Substitute.For<ITransactionTypeRepository>();
            _transactionAmountRepository = Substitute.For<ITransactionAmountRepository>();
        }

        public ITransactionRepository TransactionRepository => _transactionRepository;
        public ITransactionTypeRepository TransactionTypeRepository => _transactionTypeRepository;
        public ITransactionAmountRepository TransactionAmountRepository => _transactionAmountRepository;

        public Task SaveAsync()
        {
            return Task.CompletedTask;
        }
    }
}
