using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RespCache.Abstractions;

namespace RespCache.Extensions
{
    public static class RespCacheBuilderExtensions
    {
        public static IApplicationBuilder UseRespCache(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RespCacheMiddleware>();
        }

        public static IMvcBuilder AddRespCache(this IMvcBuilder builder, Action<RespCacheOptions> setupAction)
        {
            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            var options = new RespCacheOptions(builder);
            setupAction(options);

            builder.Services.AddSingleton(options);

            return builder;
        }
    }
}
