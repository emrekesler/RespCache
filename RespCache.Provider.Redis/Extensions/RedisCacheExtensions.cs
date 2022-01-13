using Microsoft.Extensions.DependencyInjection;
using RespCache.Abstractions;
using RespCache.Abstractions.Exception;
using RespCache.Provider.Abstractions;
using RespCache.Provider.Redis;

namespace RespCache.Provider.Redis.Extensions
{
    public static class RedisCacheExtensions
    {
        public static void UseRedis(this RespCacheOptions options)
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
