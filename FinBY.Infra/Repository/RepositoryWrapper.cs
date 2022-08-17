using FinBY.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Infra.Repository
{
    /// <summary>
    /// Wrap the Repositories to make it easier to 
    /// </summary>
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private DbContext _repoContext;
        private ITransactionRepository _transactionRepository;
        private ITransactionTypeRepository _transactionTypeRepository;
        private ITransactionAmountRepository _transactionAmountRepository;

        public RepositoryWrapper(DbContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public ITransactionRepository TransactionRepository
        {
            get
            {
                if (_transactionRepository == null)
                {
                    _transactionRepository = new TransactionRepository(_repoContext);
                }
                return _transactionRepository;
            }
        }

        public ITransactionTypeRepository TransactionTypeRepository
        {
            get
            {
                if (_transactionTypeRepository == null)
                {
                    _transactionTypeRepository = new TransactionTypeRepository(_repoContext);
                }
                return _transactionTypeRepository;
            }
        }

        public ITransactionAmountRepository TransactionAmountRepository
        {
            get
            {
                if (_transactionAmountRepository == null)
                {
                    _transactionAmountRepository = new TransactionAmountRepository(_repoContext);
                }
                return _transactionAmountRepository;
            }
        }

        public Task SaveAsync()
        {
            return _repoContext.SaveChangesAsync();
        }
    }
}
