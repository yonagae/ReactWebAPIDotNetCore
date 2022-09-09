using FinBY.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Domain.Repositories
{
    public interface ITransactionAmountRepository : IRepository<TransactionAmount, int>
    {
        public Task<List<TransactionAmount>> GetTransactionAmountsByTransactionIdAsync(int transactionID);

        public Task<List<Tuple<DateTime, string, decimal>>> GetMonthlyExpenseByPeriod(int userId, DateTime begin, DateTime end, IList<int> transactionTypeIds);
    }
}
