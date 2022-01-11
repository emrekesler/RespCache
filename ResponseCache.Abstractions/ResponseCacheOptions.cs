using Microsoft.Extensions.DependencyInjection;

namespace ResponseCache.Abstractions
{
    public class ResponseCacheOptions
    {
        public ResponseCacheOptions(IMvcBuilder builder)
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
