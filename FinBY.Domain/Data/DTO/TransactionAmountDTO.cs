using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Domain.Data.DTO
{
    public class TransactionAmountDTO : NewTransactionAmountDTO
    {
        public int Id { get; set; }
    }
}
