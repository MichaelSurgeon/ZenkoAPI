namespace ZenkoAPI.Controllers.Helpers
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string passwordHash, string inputPassword);
    }
}
