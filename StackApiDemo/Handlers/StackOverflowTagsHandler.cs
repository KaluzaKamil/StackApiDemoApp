using StackApiDemo.Models.TagsModels;
using StackApiDemo.Parameters;
using StackApiDemo.Repositories;
using StackApiDemo.StackOverflowApiIntegration;

namespace StackApiDemo.Handlers
{
    public class StackOverflowTagsHandler : IStackOverflowTagsHandler
    {
        private readonly ILogger<StackOverflowTagsHandler> _logger;
        private readonly IStackOverflowTagsRepository _repository;
        private readonly IStackOverflowTagsDownloader _downloader;

        public StackOverflowTagsHandler(ILogger<StackOverflowTagsHandler> logger, IStackOverflowTagsRepository repository, 
            IStackOverflowTagsDownloader downloader)
        {
            _logger = logger;
            _repository = repository;
            _downloader = downloader;
        }

        public async Task<int> HandleRefreshDatabaseAsync()
        {
            int recordsAdded;
            try
            {
                _logger.LogInformation("Database refresh start");

                var tagsImportsList = await _downloader.ImportStackOverflowTagsAsync();

                _repository.BeginTransaction();

                _repository.CleanDatabase();

                recordsAdded = _repository.AddTagsImports(tagsImportsList);

                _repository.CommitTransaction();

                _logger.LogInformation("Database refresh end");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while refreshing database data: ");

                _repository.RollbackTransaction();

                throw;
            }

            return recordsAdded;
        }

        public IEnumerable<Tag> HandleGet(TagParameters tagParameters)
        {
            try
            {
                return _repository.Get(tagParameters);
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "Error while getting tags from database: ");

                throw;
            }
        }

        public Tag? HandleGetByName(string name)
        {
            try
            {
                return _repository.GetByName(name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting tag \"{name}\" from database: ");

                throw;
            }
        }
    }
}
