using StackApiDemo.Repositories;
using StackApiDemo.StackOverflowApiIntegration;

namespace StackApiDemo.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> SeedDataBase(this WebApplication webApplication)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var repository = services.GetRequiredService<IStackOverflowTagsRepository>();
                var downloader = services.GetRequiredService<IStackOverflowTagsDownloader>();
                var logger = services.GetRequiredService<ILogger<WebApplication>>();

                try
                {
                    var tagsImportsList = await downloader.ImportStackOverflowTagsAsync();

                    repository.CleanDatabase();
                    repository.AddTagsImports(tagsImportsList);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error on initial tags download while starting");
                }
            }

            return webApplication;
        }
    }
}
