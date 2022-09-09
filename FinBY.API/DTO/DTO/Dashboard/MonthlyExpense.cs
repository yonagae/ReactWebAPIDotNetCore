namespace FinBY.API.DTO.DTO.Dashboard
{
    public class MonthlyExpense
    {
        public string Name { get; set; }

        public List<Dictionary<string, object>> Description { get; set; } 
    }
}
