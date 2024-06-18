using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZenkoAPI.Models
{
    public class Transaction
    {
        [Key]
        [Required]
        public required int TransactionId { get; set; }
        public required string TransactionName { get; set; } = string.Empty;
        public required decimal TransactionAmount { get; set; } = 0;
        public string TransactionLocation { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; } = DateTime.MinValue;
        [ForeignKey("CategoryId")]
        public required int CategoryId { get; set; }
        [ForeignKey("UserId")]
        public required Guid UserId { get; set; }
    }
}
