using FinBY.Domain.Data;
using FinBY.Domain.Data.PagedResult;
using FinBY.Domain.Entities;
using FinBY.Domain.Repositories;
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

        public async Task<List<Transaction>> GetAllDetailedWithouAmountsAsync()
        {
            return await _dataset.AsNoTracking()
                .Include("TransactionType")
                .Include("User")
                .ToListAsync();
        }

        public async Task<List<Transaction>> GetAllWithDetailsAsListAsync()
        {
            return await _dataset.AsNoTracking()
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

        public async Task<int> UpdateTransactionWithAmounts(Transaction transaction)
        {
            //update the transaction data on the database, on this part the amounts wont be updated
            _context.Update(transaction);
            await _context.SaveChangesAsync();

            //remove the tracker so the ClearAmounts called below don't affect the transaction object
            _context.Entry(transaction).State = EntityState.Detached;

            //get the transaction with the tracker to update            
            var transactionDB = _dataset.
              Where( x => x.Id == transaction.Id)
             .Include("TransactionType")
             .Include("TransactionAmounts")
             .Include("User")
             .FirstOrDefault();

            transactionDB.ClearAmounts();
            foreach (var t in transaction.TransactionAmounts)
                transactionDB.AddAmount(t);

            _context.Entry(transactionDB).State = EntityState.Modified;

            transaction = transactionDB;

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteCascadeToAmounts(Transaction transaction)
        {
            //Exemple when we want to execute pure SQL to be quicker and a begin and commit transaction to commit everything at once
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
                .Include("TransactionType")
                .Include("TransactionAmounts")
                .Include("User")
                .FirstOrDefaultAsync();
        }
    }
}
