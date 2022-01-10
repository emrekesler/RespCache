namespace ResponseCache.Abstractions.Exeptions
{
    public class MultipleProviderException : Exception
    {
        public MultipleProviderException(string providerName = "") : base($"Multiple Cache Provider Is Not Supported {providerName}")
        {

        }
    }
}
