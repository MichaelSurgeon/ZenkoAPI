using ZenkoAPI.Models;

namespace ZenkoAPI.Services
{
    public interface ICalculationService
    {
        Task<bool> CreateAggregatedDataAsync(Guid userId);
    }
}
