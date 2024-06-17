using System.ComponentModel.DataAnnotations;

namespace ZenkoAPI.Models
{
    public class Category
    {
        [Key]
        [Required]
        public required int CategoryId { get; set; }
        public required string CategoryName { get; set; }
    }
}
