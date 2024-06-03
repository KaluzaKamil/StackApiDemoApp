using Microsoft.AspNetCore.Mvc;
using StackApiDemo.Models.TagsModels;
using StackApiDemo.Parameters;
using StackApiDemo.Repositories;
using StackApiDemo.StackOverflowApiIntegration;
using System.Text.Json;

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

                _repository.CleanDatabaseAsync();

                recordsAdded = await _repository.AddTagsImportsAsync(tagsImportsList);

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

        public async Task<IEnumerable<Tag>> HandleGetAsync(TagParameters tagParameters)
        {
            try
            {
                return await _repository.GetTagsAsync(tagParameters);
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "Error while getting tags from database: ");

                throw;
            }
        }

        public async Task<Tag?> HandleGetByNameAsync(string name)
        {
            try
            {
                return await _repository.GetTagByNameAsync(name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting tag \"{name}\" from database: ");

                throw;
            }
        }

        public async Task<int> HandleAddTagsImportAsync(TagsImport tagsImport)
        {
            try
            {
                return await _repository.AddTagsImportsAsync(new List<TagsImport> { tagsImport });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error while adding tags import {tagsImport}");

                throw;
            }
        }

        public async Task<int> HandleDeleteTagAsync(string name)
        {
            try
            {
                return await _repository.DeleteTagAsync(name);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting tag {name}");

                throw;
            }
        }

        public async Task<int> HandleUpdateTagAsync(Tag tag)
        {
            try
            {
                return await _repository.UpdateTagAsync(tag);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error while updating tag {tag.name}");

                throw;
            }
        }
    }
}
