using ZenkoAPI.Models;

namespace ZenkoAPI.Repositories
{
    public interface ICalculationRepository
    {
        Task AddCalculatedCategoriesDataAsync(List<CalculatedCategories> aggregatedTransaction);
        Task<List<CalculatedCategories>> GetCalculatedCategoriesDataAsync(Guid userId);
        Task DeleteAggregatedCategoriesDataAsync(Guid userId);
    }
}
