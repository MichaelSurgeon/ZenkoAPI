namespace ZenkoAPI.Services
{
    public interface ICachingService
    {
        List<T> GetCache<T>(string cacheKey);
        void SetCache<T>(string cacheKey, T items);
        void ClearCache(string cacheKey);
    }
}
