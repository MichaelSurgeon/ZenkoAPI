namespace ZenkoAPI.Models
{
    public class CaculatedCategories
    {
        public required int Id { get; set; }
        public required int CategoryId { get; set; }
        public int AmountSpent { get; set; }
        public int TransactionCount { get; set; }
        public decimal PercentOfIncome { get; set; }
    }
}
