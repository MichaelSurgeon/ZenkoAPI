namespace ZenkoAPI.Models
{
    public class User
    {
        public required int UserId { get; set; }
        public required string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public required string Password { get; set; }
        public string? Address { get; set; }
    }
}
