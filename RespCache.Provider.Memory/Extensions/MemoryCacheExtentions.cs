using Microsoft.Extensions.DependencyInjection.Extensions;
using RespCache.Abstractions;
using RespCache.Abstractions.Exception;
using RespCache.Provider.Abstractions;

namespace RespCache.Provider.Memory.Extensions
{

    public static class MemoryCacheExtensions
    {
        public static void UseMemoryCache(this RespCacheOptions options)
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
