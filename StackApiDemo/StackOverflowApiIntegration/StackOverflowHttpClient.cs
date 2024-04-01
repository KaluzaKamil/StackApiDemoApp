namespace StackApiDemo.StackOverflowApiIntegration
{
    public class StackOverflowHttpClient : IDisposable, IStackOverflowHttpClient
    {
        private readonly HttpClient _httpClient;

        public StackOverflowHttpClient()
        {
            _httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.GZip });
        }

        public HttpClient GetClient()
        {
            return _httpClient;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
