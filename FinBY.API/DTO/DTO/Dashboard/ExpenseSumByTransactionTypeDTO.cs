using FinBY.API.Data.DTO;

namespace FinBY.API.Data.DTO;

public class ExpenseSumByTransactionTypeDTO
{
    public decimal Sum { get; set; }
    public TransactionTypeDTO TransactionType { get; set; }
}
