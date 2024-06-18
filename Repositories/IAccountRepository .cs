using Microsoft.AspNetCore.Mvc;
using System.Net;
using ZenkoAPI.Models;

namespace ZenkoAPI.Repositories
{
    public interface IAccountRepository
    {
        Task AddUserToDatabaseAsync(User user);
    }
}
