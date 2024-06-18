using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZenkoAPI.Models
{
    public class AggregatedTransactions
    {
        [Key]
        [Required]
        public required int Id { get; set; }
        public int TotalSpend { get; set; }
        public int TransactionCount { get; set; }
        public string MostCommonCategory { get; set; } = string.Empty;
        [ForeignKey("UserId")]
        public required Guid UserId { get; set; }
    }
}
