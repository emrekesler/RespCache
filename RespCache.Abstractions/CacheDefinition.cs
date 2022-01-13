namespace RespCache.Abstractions
{
    public class CacheDefinition
    {
        public CacheDefinition()
        {

        }

        public CacheDefinition(string key, int seconds)
        {
            Key = key;
            Seconds = seconds;
        }

        public string Key { get; set; }

        public int Seconds { get; set; }
    }
}
