using Microsoft.Extensions.DependencyInjection;
using ResponseCache.Abstractions;
using ResponseCache.Abstractions.Exception;
using ResponseCache.Provider.Abstractions;

namespace ResponseCache.Provider.Redis.Extensions
{
    public static class RedisCacheExtensions
    {
        public static void UseRedis(this ResponseCacheOptions options)
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
        }
    }
}
