using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.API.Data.DTO;

public class NewTransactionAmountDTO
{
    public int TransactionID { get; set; }
    [Range(0, 999999.99)]
    public decimal Amount { get;  set; }
    public int UserId { get;  set; }
}
