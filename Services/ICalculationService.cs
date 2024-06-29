using ZenkoAPI.Models;

namespace ZenkoAPI.Services
{
    public interface ICalculationService
    {
        Task<bool> CreateCalculatedData(Guid userId);
        Task<AggregatedTransactions> GetCalculatedTransactionDataAsync(Guid userId);
        Task<List<CalculatedCategories>> GetCalculatedCategoriesDataAsync(Guid userId);
        Task DeleteCalculatedDataAsync(Guid userId);
    }
}
