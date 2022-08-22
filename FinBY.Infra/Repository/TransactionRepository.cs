using FinBY.Domain.Data;
using FinBY.Domain.Data.PagedResult;
using FinBY.Domain.Entities;
using FinBY.Domain.Repositories;
using FinBY.Infra.Context;
using FinBY.Infra.Extensions;
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

        public Task<List<Transaction>> GetAllWithDetailsAsListAsync()
        {
            return _dataset.AsNoTracking()
                .Include("TransactionType")
                .Include("TransactionAmounts")
                .Include("User")
                .ToListAsync();
        }

        public async Task<PagedResult<Transaction>> GetAllWithDetailsAsPagedResultAsync(PagedTransactionParams transactionParams)
        {
            IQueryable<Transaction> query = _dataset.AsNoTracking()
                .Include("TransactionType")
                .Include("TransactionAmounts")
                .Include("User");
          
            query = query.AsNoTracking().OrderBy(a => a.Id);

            if (transactionParams.UserId.HasValue && transactionParams.UserId > 0)
                query = query.Where(trans => trans.UserId == transactionParams.UserId);

            if (transactionParams.TransactionType.HasValue && transactionParams.TransactionType > 0)
                query = query.Where(trans => trans.TransactionType.Id == transactionParams.TransactionType.Value);

            if (transactionParams.DataBegin.HasValue)
                query = query.Where(trans => trans.Date >= transactionParams.DataBegin);

            if (transactionParams.DataEnd.HasValue)
                query = query.Where(trans => trans.Date <= transactionParams.DataEnd);          

            return await query.GetPagedAsync(transactionParams.PageNumber, transactionParams.PageSize);
        }
    }
}
