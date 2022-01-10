using Microsoft.AspNetCore.Http;
using ResponseCache.Abstractions;
using ResponseCache.Provider.Abstractions;

namespace ResponseCache
{
    public class ResponseCacheMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICacheProvider _cacheProvider;

        public ResponseCacheMiddleware(RequestDelegate next, ICacheProvider cacheProvider)
        {
            _next = next;
            _cacheProvider = cacheProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            string cachedContent = _cacheProvider.Get(context.Request.Path.ToString());

            if (string.IsNullOrEmpty(cachedContent))
            {
                Stream body = context.Response.Body;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    context.Response.Body = memoryStream;

                    await _next(context);

                    context.Response.Body = body;

                    memoryStream.Seek(0, SeekOrigin.Begin);

                    using (StreamReader reader = new StreamReader(memoryStream))
                    {
                        string newContent = await reader.ReadToEndAsync();

                        await context.Response.WriteAsync(newContent);

                        if (context.Items["CacheOptions"] is CacheDefinition cacheDefinition)
                        {
                            _cacheProvider.Set(context.Request.Path.ToString(), newContent, TimeSpan.FromSeconds(cacheDefinition.Seconds));
                        }
                    }
                }
            }
            else
            {
                await context.Response.WriteAsync(cachedContent);
            }
        }
    }
}
