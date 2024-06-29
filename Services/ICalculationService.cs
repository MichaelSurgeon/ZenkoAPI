using ZenkoAPI.Models;

namespace ZenkoAPI.Services
{
    public interface ICalculationService
    {
        Task<bool> CreateAggregatedTransactionDataAsync(Guid userId);
        Task<AggregatedTransactions> GetAggregatedTransactionData(Guid userId);
        Task<bool> DeleteCalculatedDataAsync(Guid userId);
    }
}
