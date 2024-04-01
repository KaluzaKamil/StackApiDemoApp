
namespace StackApiDemo.StackOverflowApiIntegration
{
    public interface IStackOverflowHttpClient
    {
        public HttpClient GetClient();

        void Dispose();
    }
}