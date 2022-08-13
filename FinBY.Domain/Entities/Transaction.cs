using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Domain.Entities
{
    public class Transaction : Entity
    {
        public Transaction() 
        {
            _transactionAmounts = new List<TransactionAmount>();
        }

        public Transaction(DateTime timeStamp, TransactionType transactionType, string description, string shortDescription, List<TransactionAmount> transactionAmounts, decimal totalAmount)        {
            TimeStamp = timeStamp;
            TransactionType = transactionType;
            Description = description;
            ShortDescription = shortDescription;
            _transactionAmounts = transactionAmounts;
            TotalAmount = totalAmount;
        }

        public DateTime TimeStamp { get; private set; }
        public virtual TransactionType TransactionType { get; private set; }
        public string Description { get; private set; }
        public string ShortDescription { get; private set; }
        public decimal TotalAmount { get; private set; }

        private readonly List<TransactionAmount> _transactionAmounts;
        public IReadOnlyCollection<TransactionAmount> TransactionAmounts => _transactionAmounts;

        public bool AddTransactionAmount(TransactionAmount transactionAmount)
        {
            if (transactionAmount == null) return false;
            _transactionAmounts.Add(transactionAmount);
            TotalAmount += transactionAmount.Amount;
            return true;
        }
    }
}
