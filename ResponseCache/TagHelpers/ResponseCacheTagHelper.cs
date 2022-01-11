using Microsoft.AspNetCore.Razor.TagHelpers;
using ResponseCache.Provider.Abstractions;

namespace ResponseCache.TagHelpers
{
    public class ResponseCacheTagHelper : TagHelper
    {
        private readonly ICacheProvider _cacheProvider;

        public string Key { get; set; }

        public int Seconds { get; set; }

        public ResponseCacheTagHelper(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            var cachedContent = _cacheProvider.Get(Key);

            string content;
            if (string.IsNullOrEmpty(cachedContent))
            {
                content = (await output.GetChildContentAsync()).GetContent();

                _cacheProvider.Set(Key, content, TimeSpan.FromSeconds(Seconds));
            }
            else
            {
                content = cachedContent;
            }

            output.SuppressOutput();
            output.Content.SetHtmlContent(content);
        }
    }
}
