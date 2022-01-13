using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using RespCache.Abstractions;

namespace RespCache.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class RespCacheAttribute : ResultFilterAttribute
    {
        private readonly CacheDefinition _cacheDefinition;

        public RespCacheAttribute(CacheDefinition cacheDefinition)
        {
            _cacheDefinition = cacheDefinition;
        }

        public RespCacheAttribute(string key, int seconds)
        {
            _cacheDefinition = new CacheDefinition(key, seconds);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var options = GetOptions(context.HttpContext);

            if (options != null)
            {
                if (context.HttpContext.Items.Any(x => x.Key.ToString() == options.HttpCacheItemKey))
                {
                    context.HttpContext.Items.Remove(options.HttpCacheItemKey);
                }

                context.HttpContext.Items.Add(options.HttpCacheItemKey, _cacheDefinition);
            }

            base.OnResultExecuting(context);
        }

        private RespCacheOptions GetOptions(HttpContext httpContext)
        {
            return httpContext.RequestServices.GetService<RespCacheOptions>();
        }
    }
}
