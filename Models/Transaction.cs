namespace ZenkoAPI.Models
{
    public class Transaction
    {
        public required int TransactionId { get; set; }
        public required string TransactionName { get; set; } = string.Empty;
        public required decimal TransactionAmount { get; set; } = 0;
        public string TransactionLocation { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; } = DateTime.MinValue;
        public required int CategoryId { get; set; }
        public required int UserId { get; set; }
    }
}
