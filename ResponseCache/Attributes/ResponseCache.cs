using Microsoft.AspNetCore.Mvc.Filters;
using ResponseCache.Abstractions;

namespace ResponseCache.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class ResponseCache : ResultFilterAttribute
    {

        private readonly CacheDefinition _cacheDefinition;

        public ResponseCache(CacheDefinition cacheDefinition)
        {
            _cacheDefinition = cacheDefinition;
        }

        public ResponseCache(string key, int seconds)
        {
            _cacheDefinition = new CacheDefinition { Key = key, Seconds = seconds };
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.HttpContext.Items.Any(x => x.Key.ToString() == "CacheOptions"))
            {
                context.HttpContext.Items.Remove("CacheOptions");
            }

            context.HttpContext.Items.Add("CacheOptions", _cacheDefinition);

            base.OnResultExecuting(context);
        }
    }
}
