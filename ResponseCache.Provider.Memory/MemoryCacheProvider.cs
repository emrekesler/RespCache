using Microsoft.Extensions.Caching.Memory;
using ResponseCache.Provider.Abstractions;

namespace ResponseCache.Provider.Memory
{
    internal class MemoryCacheProvider : ICacheProvider
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheProvider(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T Get<T>(string key) where T : class, new()
        {
            return _memoryCache.Get<T>(key);
        }

        public string Get(string key)
        {
            return (string)_memoryCache.Get(key);
        }

        public void Set<T>(string key, T value, TimeSpan timeSpan)
        {
            _memoryCache.Set(key, value, timeSpan);
        }

        public void Set(string key, string value, TimeSpan timeSpan)
        {
            _memoryCache.Set(key, value, timeSpan);
        }
    }
}
