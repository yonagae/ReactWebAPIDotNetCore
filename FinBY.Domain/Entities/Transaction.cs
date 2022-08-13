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
        private readonly List<TransactionAmount> _transactionAmounts;

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "TimeStamp must be specified")]
        public DateTime TimeStamp { get; private set; }

        [Required(ErrorMessage = "Description must be specified")]
        public virtual TransactionType TransactionType { get; private set; }

        [Required(ErrorMessage = "User must be specified")]
        public virtual int UserId { get; private set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Description must be specified")]     
        public string Description { get; private set; }

        [StringLength(50)]
        [Required(ErrorMessage = "ShortDescription must be specified")]
        public string ShortDescription { get; private set; }

        [Required(ErrorMessage = "Total Ammount must be specified")]
        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; private set; }

        [MinLength(1)]
        [Required(ErrorMessage = "TransactionAmount must be specified")]
        public IReadOnlyCollection<TransactionAmount> TransactionAmounts => _transactionAmounts;


        public Transaction()
        {
            _transactionAmounts = new List<TransactionAmount>();
        }

        public Transaction(TransactionType transactionType, int userId, string description, string shortDescription, List<TransactionAmount> transactionAmounts, decimal totalAmount)
        {
            TimeStamp = DateTime.Now;
            TransactionType = transactionType;
            Description = description;
            ShortDescription = shortDescription;
            _transactionAmounts = transactionAmounts;
            TotalAmount = totalAmount;
            UserId = userId;
        }
    }
}
