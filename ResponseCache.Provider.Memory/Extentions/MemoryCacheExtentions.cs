using Microsoft.Extensions.DependencyInjection.Extensions;
using ResponseCache.Abstractions;
using ResponseCache.Abstractions.Exeptions;
using ResponseCache.Provider.Abstractions;

namespace ResponseCache.Provider.Memory.Extentions
{

    public static class MemoryCacheExtentions
    {
        public static ResponseCacheOptions UseMemoryCache(this ResponseCacheOptions options)
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

            return options;
        }

    }
}
