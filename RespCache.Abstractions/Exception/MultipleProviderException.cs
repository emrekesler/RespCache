namespace RespCache.Abstractions.Exception
{
    public class MultipleProviderException : System.Exception
    {
        public MultipleProviderException(string providerName = "") : base($"Multiple Cache Provider Is Not Supported {providerName}")
        {

        }
    }
}
