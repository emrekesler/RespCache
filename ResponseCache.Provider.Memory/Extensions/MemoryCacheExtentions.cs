using Microsoft.Extensions.DependencyInjection.Extensions;
using ResponseCache.Abstractions;
using ResponseCache.Abstractions.Exception;
using ResponseCache.Provider.Abstractions;

namespace ResponseCache.Provider.Memory.Extensions
{

    public static class MemoryCacheExtensions
    {
        public static void UseMemoryCache(this ResponseCacheOptions options)
        {
            if (options.Builder == null)
            {
                throw new ArgumentNullException();
            }

            if (options.Builder.Services.Any(s => s.ServiceType.Name == "ICacheProvider"))
            {
                throw new MultipleProviderException("MemoryCache");
            }

            options.Builder.Services.TryAddSingleton<ICacheProvider, MemoryCacheProvider>();
        }

    }
}
