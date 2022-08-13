using FinBY.Domain.Entities;
using FinBY.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinBY.Infra.Repository
{
    public class TransactionTypeRepository : Repository<TransactionType, int>, ITransactionTypeRepository
    {
        public TransactionTypeRepository(DbContext context)
           : base(context)
        {
        }
    }
}
