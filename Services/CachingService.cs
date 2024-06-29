using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace ZenkoAPI.Services
{
    public class CachingService : ICachingService
    {
        private readonly IMemoryCache _memoryCache;

        public CachingService(IMemoryCache cache)
        {
            _memoryCache = cache;
        }
        public List<T> GetCache<T>(string cacheKey) => _memoryCache.Get<List<T>>(cacheKey) ?? [];

        public void SetCache<T>(string cacheName, T items) => _memoryCache.Set(cacheName, items, TimeSpan.FromMinutes(1));

        public void ClearCache(string cacheKey) => _memoryCache.Remove(cacheKey);
    }
}
