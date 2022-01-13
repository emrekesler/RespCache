using Microsoft.AspNetCore.Http;
using RespCache.Abstractions;
using RespCache.Provider.Abstractions;

namespace RespCache
{
    public class RespCacheMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICacheProvider _cacheProvider;
        private readonly RespCacheOptions _options;

        public RespCacheMiddleware(RequestDelegate next, ICacheProvider cacheProvider, RespCacheOptions options)
        {
            _next = next;
            _cacheProvider = cacheProvider;
            _options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            string cacheKey = context.Request.Path.ToString();

            string cachedContent = _cacheProvider.Get(cacheKey);

            if (string.IsNullOrEmpty(cachedContent))
            {
                Stream body = context.Response.Body;

                using (MemoryStream memoryStream = new())
                {
                    context.Response.Body = memoryStream;

                    await _next(context);

                    context.Response.Body = body;

                    memoryStream.Seek(0, SeekOrigin.Begin);

                    using (StreamReader reader = new(memoryStream))
                    {
                        string content = await reader.ReadToEndAsync();

                        await context.Response.WriteAsync(content);

                        CacheDefinition cacheDefinition = GetCacheDefinition(context, cacheKey);

                        if (cacheDefinition != null)
                        {
                            _cacheProvider.Set(context.Request.Path.ToString(), content, TimeSpan.FromSeconds(cacheDefinition.Seconds));
                        }
                    }
                }
            }
            else
            {
                await context.Response.WriteAsync(cachedContent);
            }
        }

        private CacheDefinition GetCacheDefinition(HttpContext context, string cacheKey)
        {
            if (_options.PathDefinitions.Any(pd => pd.Key == cacheKey))
            {
                return _options.PathDefinitions.First(pd => pd.Key == cacheKey);
            }

            if (context.Items[_options.HttpCacheItemKey] is CacheDefinition cacheDefinition)
            {
                return cacheDefinition;
            }

            return null;
        }
    }
}
