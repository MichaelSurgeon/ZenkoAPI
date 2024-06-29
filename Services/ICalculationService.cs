using ZenkoAPI.Models;

namespace ZenkoAPI.Services
{
    public interface ICalculationService
    {
        Task<bool> CreateCalculatedData(Guid userId);
        Task<AggregatedTransactions> GetCalculatedData(Guid userId);
        Task<bool> DeleteCalculatedDataAsync(Guid userId);
    }
}
