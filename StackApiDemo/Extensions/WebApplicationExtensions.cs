using StackApiDemo.Repositories;
using StackApiDemo.StackOverflowApiIntegration;

namespace StackApiDemo.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> InitDataBase(this WebApplication webApplication)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var repository = services.GetService<IStackOverflowTagsRepository>();
                var downloader = services.GetService<IStackOverflowTagsDownloader>();
                var logger = services.GetService<ILogger<WebApplication>>();

                try
                {
                    var tagsImportsList = await downloader.ImportStackOverflowTagsAsync();

                    repository.CleanDatabase();
                    repository.AddTagsImports(tagsImportsList);
                }
                catch (Exception ex)
                {
                    logger.LogError("Error on initial tags download while starting", ex.Message);
                }
            }

            return webApplication;
        }
    }
}
