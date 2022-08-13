using FinBY.Domain.Entities;
using FinBY.Domain.Queries;
using FinBY.Domain.Repositories;
using FinBY.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinBY.Infra.Repository
{
    public class TransactionAmountRepository : Repository<TransactionAmount, int>, ITransactionAmountRepository
    {
        public TransactionAmountRepository(DbContext context)
           : base(context)
        {
        }

        public async Task<List<TransactionAmount>> GetTransactionAmountsByTransactionIdAsync(int transactionID)
        {
            return await _dataset.AsNoTracking().Where(TransactionAmountQueries.GetByTransactionId(transactionID)).ToListAsync();
        }
    }
}
