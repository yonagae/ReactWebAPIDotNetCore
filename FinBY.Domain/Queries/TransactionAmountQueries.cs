using FinBY.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Domain.Queries
{
    public class TransactionAmountQueries
    {
        /// <summary>
        /// The time portion of the parameter will be ignored. 
        /// </summary>
        /// <param name="begin">beginning of the period (>=)</param>
        /// <param name="end">ending of the period (<=)</param>
        /// <returns></returns>
        public static Expression<Func<TransactionAmount, bool>> GetByTransactionId(int id)
        {
            return x => x.TransactionId == id;
        }
    }
}
