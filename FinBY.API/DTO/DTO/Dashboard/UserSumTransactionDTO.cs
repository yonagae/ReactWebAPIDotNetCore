using FinBY.Domain.Enum;

namespace FinBY.API.Data.DTO;

public class UserSumTransactionDTO
{
    public string UserName { get; set; }

    public Dictionary<eTransactionFlow, decimal> SumByFlowType { get; set; } = new Dictionary<eTransactionFlow, decimal>();
}
