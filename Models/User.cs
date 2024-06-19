using System.ComponentModel.DataAnnotations;

namespace ZenkoAPI.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Password { get; set; } = string.Empty;
        public string? Address { get; set; }
    }
}
