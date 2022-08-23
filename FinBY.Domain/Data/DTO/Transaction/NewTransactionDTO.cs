using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Domain.Data.DTO
{
    public class NewTransactionDTO
    {        
        public int TransactionTypeId { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public int UserId { get; set; }
        public ICollection<TransactionAmountDTO> TransactionAmounts { get; set; }

        [Range(0, 999999.99)]
        public Decimal TotalAmount { get; set; }

        public DateTime Date { get; set; }
    }
}
