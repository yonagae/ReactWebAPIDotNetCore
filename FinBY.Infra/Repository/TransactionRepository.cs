using FinBY.Domain.Entities;
using FinBY.Domain.Repositories;
using FinBY.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinBY.Infra.Repository
{
    public class TransactionRepository : Repository<Transaction, int>, ITransactionRepository
    {
        public TransactionRepository(DbContext context)
           : base(context)
        {
        }

        public Task<List<Transaction>> GetAllWithDetailsAsList()
        {
            return _dataset.AsNoTracking()
                .Include("TransactionType")
                .Include("TransactionAmounts")
                .Include("User")
                .ToListAsync();
        }
    }
}
