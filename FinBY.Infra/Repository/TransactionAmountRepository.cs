using FinBY.Domain.Entities;
using FinBY.Domain.Queries;
using FinBY.Domain.Repositories;
using FinBY.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
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
     

        public  Task<List<TransactionAmount>> GetTransactionAmountsByTransactionIdAsync(int transactionID)
        {
            return _dataset.AsNoTracking().Where(TransactionAmountQueries.GetByTransactionId(transactionID)).ToListAsync();
        }

        public async Task<List<Tuple<DateTime, string, decimal>>> GetMonthlyExpenseByPeriod(int userId, DateTime begin, DateTime end, IList<int> transactionTypeIds)
        {
            var sums = await _dataset.AsNoTracking()
                  .Where(x => 
                  x.UserId == userId &&
                  x.Transaction.Date >= begin && x.Transaction.Date <= end
                  && transactionTypeIds.Contains(x.Transaction.TransactionTypeId)
                  )
                  .GroupBy(x => new { x.Transaction.Date.Year, x.Transaction.Date.Month, x.Transaction.TransactionTypeId })
                  .Select(x => new Tuple<DateTime, string, decimal>(
                       x.First().Transaction.Date,
                       x.First().Transaction.TransactionType.Name,
                       x.Sum(y => y.PositiveAmount)
                      )).ToListAsync();

            return sums;
        }
     }
}
