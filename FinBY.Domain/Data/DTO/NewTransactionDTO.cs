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
        public DateTime TimeStamp { get; set; }
        public TransactionTypeDTO TransactionType { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public ICollection<TransactionAmountDTO> TransactionAmounts { get; set; }

        [Range(0, 999999.99)]
        public Decimal TotalAmount { get; set; }
    }
}
