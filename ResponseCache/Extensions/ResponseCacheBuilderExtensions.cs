using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ResponseCache.Abstractions;

namespace ResponseCache.Extensions
{
    public static class ResponseCacheBuilderExtensions
    {
        public static IApplicationBuilder UseResponseCache(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseCacheMiddleware>();
        }

        public static IMvcBuilder AddResponseCache(this IMvcBuilder builder, Action<ResponseCacheOptions> setupAction)
        {
            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            var options = new ResponseCacheOptions(builder);
            setupAction(options);

            builder.Services.AddSingleton(options);

            return builder;
        }
    }
}
