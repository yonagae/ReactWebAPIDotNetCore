using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FinBY.Domain.Entities
{
    public  class TransactionAmount : Entity
    {
        [Required(ErrorMessage = "Amount must be specified")]
        public decimal Amount { get; private set; }

        [Required(ErrorMessage = "TransactionId must be specified")]
        public int TransactionId { get; private set; }

        [Required(ErrorMessage = "User must be specified")]
        public int UserId { get; private set; }

        public User User { get; private set; }

        public TransactionAmount(int transactionId, int userId, decimal amount)
        {
            Amount = amount;
            TransactionId = transactionId;
            UserId = userId;
        }

        public TransactionAmount() { }
    }
}
