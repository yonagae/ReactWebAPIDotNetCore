using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.API.Data.DTO
{
    public class TransactionDTO : NewTransactionDTO
    {
        public int Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public UserDTO User { get; set; }
        public TransactionTypeDTO TransactionType { get; set; }
    }
}
