namespace ZenkoAPI.Dtos
{
    public class CalculatedCategoriesResponse
    {
        public string Category {  get; set; }
        public decimal AmountSpent { get; set; }
        public decimal TransactionCount { get; set; }
        public decimal PercentOfIncome { get; set; }
        public string Status { get; set; }
    }
}
