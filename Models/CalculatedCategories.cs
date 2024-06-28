using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZenkoAPI.Models
{
    public class CalculatedCategories
    {
        [Key]
        [Required]
        public required Guid Id { get; set; }
        [ForeignKey("CategoryId")]
        public required int CategoryId { get; set; }
        public int AmountSpent { get; set; }
        public int TransactionCount { get; set; }
        public decimal PercentOfIncome { get; set; }
    }
}
