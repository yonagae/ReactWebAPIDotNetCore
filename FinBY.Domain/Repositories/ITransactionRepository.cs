using FinBY.Domain.Data;
using FinBY.Domain.Data.PagedResult;
using FinBY.Domain.Entities;
using FinBY.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Domain.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction, int>
    {
        public void Add(IList<Transaction> transactions);
        public Task<Transaction> GetDetailedByIdAsync(int id);
        public Task<List<Transaction>> GetAllDetailedWithoutAmountsAsync(DateTime start, DateTime end);
        public Task<PagedResult<Transaction>> GetAllWithDetailsAsPagedResultAsync(PagedTransactionParams transsactionParams);
        public Task<int> UpdateTransactionWithAmounts(Transaction transaction);
        public Task<List<Tuple<TransactionType, decimal>>> GetSumOfTransactionsByTypeByPeriod(DateTime begin, DateTime end);
        public Task<List<Tuple<User, eTransactionFlow, decimal>>> GetSumOfTransactionsByUserByPeriod(DateTime begin, DateTime end);
        public Task<List<Tuple<DateTime, TransactionType, decimal>>> GetMonthlyExpenseByPeriod(DateTime begin, DateTime end, IList<int> transactionTypeIds);

        public Task<List<Tuple<DateTime, string, decimal>>> GetMonthlyExpenseByPeriod(int userId, DateTime begin, DateTime end, IList<int> transactionTypeIds);
    }
}
