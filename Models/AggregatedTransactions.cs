using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZenkoAPI.Models
{
    public class AggregatedTransactions
    {
        [Key]
        [Required]
        public required Guid Id { get; set; }
        public decimal TotalSpend { get; set; }
        public int TransactionCount { get; set; }
        public string MostCommonCategory { get; set; } = string.Empty;
        [ForeignKey("UserId")]
        public required Guid UserId { get; set; }
    }
}
