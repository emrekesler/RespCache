namespace ResponseCache.Provider.Abstractions
{
    public interface ICacheProvider
    {
        public T Get<T>(string key) where T : class, new();

        public string Get(string key);

        public void Set<T>(string key, T value, TimeSpan timeSpan);

        public void Set(string key, string value, TimeSpan timeSpan);

    }
}
