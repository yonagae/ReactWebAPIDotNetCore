using FinBY.Domain.Data;
using FinBY.Domain.Data.PagedResult;
using FinBY.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Domain.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction, int>
    {
        public Task<Transaction> GetDetailedByIdAsync(int id);

        public Task<List<Transaction>> GetAllDetailedWithouAmountsAsync();

        public Task<List<Transaction>> GetAllWithDetailsAsListAsync();

        public Task<PagedResult<Transaction>> GetAllWithDetailsAsPagedResultAsync(PagedTransactionParams transsactionParams);

        public Task<int> UpdateTransactionWithAmounts(Transaction transaction);

        public Task<List<Tuple<TransactionType, decimal>>> GetSumOfTransactionsByTypeByPeriod(DateTime begin, DateTime end);
    }
}
