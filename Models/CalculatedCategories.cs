using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZenkoAPI.Models
{
    public class CalculatedCategories
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public decimal AmountSpent { get; set; }
        public int TransactionCount { get; set; }
        public decimal PercentOfIncome { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
    }
}
