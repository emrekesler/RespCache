using Microsoft.Extensions.DependencyInjection;

namespace RespCache.Abstractions
{
    public class RespCacheOptions
    {
        public RespCacheOptions(IMvcBuilder builder)
        {
            Builder = builder;
            HttpCacheItemKey = "CacheOptions";
            PathDefinitions = new List<CacheDefinition>();
        }

        public IMvcBuilder Builder { get; set; }

        public string HttpCacheItemKey { get; set; }

        public List<CacheDefinition> PathDefinitions { get; set; }

    }
}
