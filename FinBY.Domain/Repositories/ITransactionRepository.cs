using FinBY.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Domain.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction, int>
    {
        public Task<List<Transaction>> GetAllWithDetailsAsList();
    }
}
