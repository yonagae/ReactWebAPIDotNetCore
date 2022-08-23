using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Domain.Entities
{
    public partial class Transaction : Entity
    {
        public bool AddAmount(TransactionAmount transactionAmount)
        {
            if (transactionAmount == null) return false;

            _transactionAmounts.Add(transactionAmount);
            TotalAmount += transactionAmount.Amount;

            return true;
        }

        public bool AddAmounts(IList<TransactionAmount> transactionAmounts)
        {
            if (transactionAmounts == null) return false;

            foreach (var transactionAmount in transactionAmounts)
                this.AddAmount(transactionAmount);

            return true;
        }               

        public bool RemoveTransactionAmount(TransactionAmount transactionAmount)
        {
            if (transactionAmount == null) return false;
            if (transactionAmount.TransactionId != this.Id) return false;

            _transactionAmounts.Remove(transactionAmount);
            TotalAmount -= transactionAmount.Amount;

            return true;
        }
    }
}
