namespace ZenkoAPI.Models
{
    public class AggregatedTransactions
    {
        public required int Id { get; set; }
        public int TotalSpend { get; set; }
        public int TransactionCount { get; set; }
        public string MostCommonCategory { get; set; } = string.Empty;
        public required int UserId { get; set; }
    }
}
