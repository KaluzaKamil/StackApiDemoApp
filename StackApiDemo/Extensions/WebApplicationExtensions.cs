using Microsoft.EntityFrameworkCore;
using StackApiDemo.Contexts;
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

                    await repository.CleanDatabaseAsync();
                    await repository.AddTagsImportsAsync(tagsImportsList);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error on initial tags download while starting");
                }
            }

            return webApplication;
        }

        public static WebApplication MigrateDatabase(this WebApplication webApplication)
        {
            using (var serviceScope = webApplication.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                var db = serviceScope.ServiceProvider.GetRequiredService<StackOverflowTagsContext>().Database;

                logger.LogInformation("Database migration start");
                while (!db.CanConnect())
                {
                    logger.LogInformation("Connecting...");
                    Thread.Sleep(1000);
                }

                try
                {
                    serviceScope.ServiceProvider.GetRequiredService<StackOverflowTagsContext>().Database.Migrate();
                    logger.LogInformation("Database migrated");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error while migrating database");
                }
            }
            return webApplication;
        }
    }
}
