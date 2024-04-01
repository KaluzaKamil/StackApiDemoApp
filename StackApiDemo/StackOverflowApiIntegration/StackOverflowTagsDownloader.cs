using StackApiDemo.Models.TagsModels;
using System.Text.Json;

namespace StackApiDemo.StackOverflowApiIntegration
{
    public class StackOverflowTagsDownloader : IStackOverflowTagsDownloader
    {
        private readonly IStackOverflowHttpClient _httpClient;

        public StackOverflowTagsDownloader(IStackOverflowHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TagsImport>> ImportStackOverflowTagsAsync()
        {
            var tagImportsList = new List<TagsImport>();
            var httpClient = _httpClient.GetClient();

            httpClient.BaseAddress = new Uri("https://api.stackexchange.com/");
            httpClient.DefaultRequestHeaders.Accept.Clear();

            for (var i = 1; i <= 10; i++)
            {
                var response = await httpClient.GetAsync($"2.3/tags?page={i}&pagesize=100&order=desc&sort=popular&site=stackoverflow");
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();

                var tags = JsonSerializer.Deserialize<TagsImport>(responseBody);
                tagImportsList.Add(tags);
            }

            return tagImportsList;
        }
    }
}
