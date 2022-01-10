using Microsoft.Extensions.DependencyInjection;
using ResponseCache.Abstractions;
using ResponseCache.Abstractions.Exeptions;
using ResponseCache.Provider.Abstractions;

namespace ResponseCache.Provider.Redis.Extentions
{
    public static class RedisCacheExtentions
    {
        public static ResponseCacheOptions UseRedis(this ResponseCacheOptions options)
        {
            if (options.Builder == null)
            {
                throw new ArgumentNullException();
            }

            if (options.Builder.Services.Any(s => s.ServiceType.Name == "ICacheProvider"))
            {
                throw new MultipleProviderException("Redis");
            }

            options.Builder.Services.AddSingleton<ICacheProvider, RedisCacheProvider>();

            return options;
        }

    }
}
