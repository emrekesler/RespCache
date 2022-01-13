using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RespCache.Provider.Abstractions;
using StackExchange.Redis;

namespace RespCache.Provider.Redis
{
    public class RedisCacheProvider : ICacheProvider
    {
        private readonly IDatabase _db;

        public RedisCacheProvider(IConfiguration configuration)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(
            new ConfigurationOptions
            {
                EndPoints = { configuration["RespCache:Redis:ConnectionString"] }
            });

            _db = redis.GetDatabase();
        }

        public T Get<T>(string key) where T : class, new()
        {
            string value = _db.StringGet(key);
            var item = JsonConvert.DeserializeObject<T>(value);
            return item;
        }

        public string Get(string key)
        {
            return _db.StringGet(key);
        }

        public void Set<T>(string key, T value, TimeSpan timeSpan)
        {
            string item = JsonConvert.SerializeObject(value);
            _db.StringSet(key, item, timeSpan);
        }

        public void Set(string key, string value, TimeSpan timeSpan)
        {
            _db.StringSet(key, value, timeSpan);
        }
    }
}
