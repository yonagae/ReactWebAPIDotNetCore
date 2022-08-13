using System;
using System.Collections.Generic;
using System.Text;

namespace FinBY.Domain.Entities
{
    public  class TransactionAmount : Entity
    {
        public TransactionAmount(int transactionId, User usuario, decimal amount)
        {
            Amount = amount;
            TransactionId = transactionId;
            User = usuario;
        }

        public TransactionAmount() { }

        public decimal Amount { get; private set; }
        public int TransactionId { get; private set; }
        public User User { get; private set; }
    }
}
