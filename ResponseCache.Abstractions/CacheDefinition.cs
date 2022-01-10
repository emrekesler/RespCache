namespace ResponseCache.Abstractions
{
    public class CacheDefinition
    {
        public string Key { get; set; }

        public int Seconds { get; set; }
    }
}
