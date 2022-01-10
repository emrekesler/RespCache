using Microsoft.Extensions.DependencyInjection;

namespace ResponseCache.Abstractions
{
    public class ResponseCacheOptions
    {
        public ResponseCacheOptions(IMvcBuilder builder)
        {
            Builder = builder;
        }

        public IMvcBuilder Builder { get; set; }
    }
}
