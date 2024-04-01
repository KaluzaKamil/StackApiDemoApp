﻿using StackApiDemo.Models.TagsModels;
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
                _logger.LogError("Error occured while refreshing database data: " + ex.Message);

                _repository.RollbackTransaction();

                throw;
            }

            return recordsAdded;
        }

        public IEnumerable<Tag> HandleGetTags(TagParameters tagParameters)
        {
            try
            {
                return _repository.GetTags(tagParameters);
            }
            catch(Exception ex) 
            {
                _logger.LogError("Error while getting tags from database: " + ex.Message);

                throw;
            }
        }
    }
}