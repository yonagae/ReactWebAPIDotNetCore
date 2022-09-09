using FinBY.Domain.Data;
using FinBY.Domain.Data.PagedResult;
using FinBY.Domain.Entities;
using FinBY.Domain.Enum;
using FinBY.Domain.Repositories;
using FinBY.Infra.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public void Add(IList<Transaction> transactions)
        {
            _dataset.AddRange(transactions);
        }

        public async Task<List<Transaction>> GetAllDetailedWithoutAmountsAsync(DateTime start, DateTime end)
        {
             return await _dataset.AsNoTracking()
                .Where(x => x.Date >= start && x.Date <= end )
                .Include(x => x.TransactionType)
                .Include(x => x.User)
                .ToListAsync(); ;
        }

        public async Task<PagedResult<Transaction>> GetAllWithDetailsAsPagedResultAsync(PagedTransactionParams transactionParams)
        {
            IQueryable<Transaction> query = _dataset.AsNoTracking()
               .Include(x => x.TransactionType)
                .Include(x => x.TransactionAmounts)
                .Include(x => x.User);
          
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

        public async Task<int> UpdateTransactionWithAmounts(Transaction transaction)
        {
            //remove the tracker so the ClearAmounts called below don't affect the transaction object
            _context.Entry(transaction).State = EntityState.Detached;

            //get the existing transaction with the tracker to update            
            var existingTransaction = _dataset.
              Where( x => x.Id == transaction.Id)
             .Include(x => x.TransactionType)
             .Include(x => x.TransactionAmounts)
             .Include(x => x.User)
             .SingleOrDefault();

            // update existingTransaction using the data from the transaction 
            _context.Entry(existingTransaction).CurrentValues.SetValues(transaction);

            // update existing transactions amounts
            existingTransaction.ClearAmounts();
            foreach (var t in transaction.TransactionAmounts)
                existingTransaction.AddAmount(t);

            _context.Entry(existingTransaction).State = EntityState.Modified;

            transaction = existingTransaction;

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteCascadeToAmounts(Transaction transaction)
        {
            //Example when we want to execute pure SQL to be quicker and a begin and commit transaction to commit everything at once
            await _context.Database.BeginTransactionAsync();

            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"DELETE from dbo.[TransactionAmount] where [TransactionId] = {transaction.Id}");
            await _context.SaveChangesAsync();

            await _context.Database.CommitTransactionAsync();
            return true;

        }

        public async Task<Transaction> GetDetailedByIdAsync(int id)
        {
            return await _dataset.AsNoTracking()
                .Where( x => x.Id == id)
                .Include(x => x.TransactionType)
                .Include(x => x.TransactionAmounts)
                .Include(x => x.User)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Tuple<TransactionType, decimal>>> GetSumOfTransactionsByTypeByPeriod(DateTime begin, DateTime end)
        {
            var sums = await _dataset.AsNoTracking()
                  .Where(x => x.Date >= begin && x.Date <= end && x.Flow == eTransactionFlow.Credit)
                  .GroupBy(x => x.TransactionTypeId)
                  .Select(x => new Tuple<TransactionType, decimal>(
                       x.First().TransactionType,
                       x.Sum(y => y.TotalAmount) 
                  )).
                  ToListAsync();

            return sums;
        }

        public async Task<List<Tuple<User, eTransactionFlow, decimal>>> GetSumOfTransactionsByUserByPeriod(DateTime begin, DateTime end)
        {
            var sums = await _dataset.AsNoTracking()
                  .Where(x => x.Date >= begin && x.Date <= end)
                  .GroupBy(x => new { x.UserId, x.Flow })
                  .Select(x => new Tuple<User, eTransactionFlow, decimal>(
                       x.First().User,
                       x.First().Flow,
                       x.Sum(y => y.TotalAmount)
                  )).
                  ToListAsync();

            return sums;
        }

        public async Task<List<Tuple<DateTime, TransactionType, decimal>>> GetMonthlyExpenseByPeriod(DateTime begin, DateTime end, IList<int> transactionTypeIds)
        {
            var sums = await _dataset.AsNoTracking()
                  .Where(x => x.Date >= begin && x.Date <= end && transactionTypeIds.Contains(x.TransactionTypeId))
                  .GroupBy(x => new { x.Date.Year, x.Date.Month , x.TransactionTypeId })
                  .Select(x => new Tuple<DateTime, TransactionType, decimal>(
                       x.First().Date,
                       x.First().TransactionType,
                       x.Sum(y => y.TotalAmount)
                  ))
                  .ToListAsync();

            return sums.OrderBy(x => x.Item1).ToList();
        }

        public async Task<List<Tuple<DateTime, string, decimal>>> GetMonthlyExpenseByPeriod(int userId, DateTime begin, DateTime end, IList<int> transactionTypeIds)
        {
            var sums = await _dataset.AsNoTracking()
                  .Where(x => x.Date >= begin && x.Date <= end
                  && transactionTypeIds.Contains(x.TransactionTypeId)
                  && x.TransactionAmounts.Where(t => t.UserId == userId).Count() > 0
                  )
                  .Select(x => new Tuple<DateTime, string, IReadOnlyCollection<TransactionAmount>>(
                       x.Date,
                       x.TransactionType.Name,
                       x.TransactionAmounts
                      )).ToListAsync();


             var result = sums.GroupBy(x => new { x.Item1.Year, x.Item1.Month, x.Item3 })
                  .Select(y => new Tuple<DateTime, string, decimal>(
                       y.First().Item1,
                       y.First().Item2,
                       y.Sum(y => y.Item3.Where(t => t.UserId == userId).Select(p => p.PositiveAmount).First())
                  ))
                  .ToList();

            return result.OrderBy(x => x.Item1).ToList();
        }
    }
}
