using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Domain.Repositories
{
    public interface IRepositoryWrapper
    {
        ITransactionRepository TransactionRepository { get; }
        ITransactionTypeRepository TransactionTypeRepository { get; }
        ITransactionAmountRepository TransactionAmountRepository { get; }
        Task SaveAsync();
    }
}
