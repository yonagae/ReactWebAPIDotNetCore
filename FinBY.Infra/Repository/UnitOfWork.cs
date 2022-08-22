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
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DbContext _repoContext;
        private ITransactionRepository _transactionRepository;
        private ITransactionTypeRepository _transactionTypeRepository;
        private ITransactionAmountRepository _transactionAmountRepository;
        private IUserRepository _userRepository;

        public UnitOfWork(DbContext repositoryContext)
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

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_repoContext);
                }
                return _userRepository;
            }
        }


        public Task SaveAsync()
        {
            return _repoContext.SaveChangesAsync();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _repoContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
