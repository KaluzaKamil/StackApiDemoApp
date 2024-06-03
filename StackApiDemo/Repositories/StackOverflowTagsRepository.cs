using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.OpenApi.Validations;
using StackApiDemo.Contexts;
using StackApiDemo.Extensions;
using StackApiDemo.Models.TagsModels;
using StackApiDemo.Parameters;

namespace StackApiDemo.Repositories
{
    public class StackOverflowTagsRepository : IStackOverflowTagsRepository
    {
        private readonly StackOverflowTagsContext _context;
        private readonly ILogger<StackOverflowTagsRepository> _logger;

        public StackOverflowTagsRepository(StackOverflowTagsContext context, ILogger<StackOverflowTagsRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Tag>> GetTagsAsync(TagParameters tagParameters)
        {
            var tags = _context.Tags
                .OrderByPropertyName(tagParameters.OrderByProperty, tagParameters.OrderByAscending)
                .Skip((tagParameters.PageNumber - 1) * tagParameters.PageSize)
                .Take(tagParameters.PageSize)
                .ToList();

            tags.ForEach(t => t.collectives = GetCollectivesByTagId(t.Id).ToList());
            tags.ForEach(t => t.collectives?.ToList().ForEach(c => c.external_links = GetExternalLinksByCollectiveId(c.Id).ToList()));
                
            return tags;
        }

        public async Task<Tag?> GetTagByNameAsync(string queriedName)
        {
            var tag = _context.Tags
                .Where(t => t.name == queriedName)
                .FirstOrDefault();
           
            if(tag != null)
            {
                tag.collectives = GetCollectivesByTagId(tag.Id).ToList();
                tag.collectives?.ToList().ForEach(c => c.external_links = GetExternalLinksByCollectiveId(c.Id).ToList());
                return tag;
            }
            else
            {
                _logger.LogWarning($"Queried tag \"{queriedName}\" does not exist in the database, returning null.");
                return null;
            }
        }

        public IEnumerable<Collective> GetCollectivesByTagId(Guid TagId) 
        {
            return _context.Collectives.Where(c => c.TagId == TagId);
        }

        public IEnumerable<ExternalLink> GetExternalLinksByCollectiveId(Guid CollectiveId) 
        {
            return _context.ExternalLinks.Where(el => el.CollectiveId == CollectiveId);
        }

        public async Task<int> AddTagsImportsAsync(IEnumerable<TagsImport> tagsImports)
        {
            var tagsPopulation = tagsImports.SelectMany(t => t.items).Sum(i => i.count) + _context.Tags.Sum(t => t.count);

            foreach (var tagsImport in tagsImports)          
                ProcessTagsImport(tagsImport, tagsPopulation);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteTagAsync(string name)
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(t => t.name == name);

            if (tag != null)
            {
                _context.Tags.Remove(tag);
                UpdateTagsShares(_context.Tags.Sum(t => t.count));
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateTagAsync(Tag tag)
        {
            var tagToUpdate = await _context.Tags.FirstOrDefaultAsync(t => t.name == tag.name);

            if(tagToUpdate != null)
            {
                tagToUpdate.is_required = tag.is_required;
                tagToUpdate.has_synonyms = tag.has_synonyms;
                tagToUpdate.count = tag.count;
                tagToUpdate.collectives = tag.collectives;
                tagToUpdate.is_moderator_only = tag.is_moderator_only;

                UpdateTagsShares(_context.Tags.Sum(t => t.count));
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> CleanDatabaseAsync()
        {
            _context.ExternalLinks.RemoveRange(_context.ExternalLinks);
            _context.Collectives.RemoveRange(_context.Collectives);
            _context.Tags.RemoveRange(_context.Tags);

            return await _context.SaveChangesAsync();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }

        private void ProcessTagsImport(TagsImport tagsImport, int tagsPopulation)
        {
            foreach (var tag in tagsImport.items)
            {
                tag.TagsImportId = tagsImport.Id;
                tag.TagsImport = tagsImport;
                tag.share = ((decimal)tag.count / (decimal)tagsPopulation) * 100;

                if (tag.collectives != null)
                {
                    foreach (var collective in tag.collectives)
                    {
                        collective.TagId = tag.Id;
                        collective.Tag = tag;
                        collective.external_links.ToList().ForEach(el => el.Collective = collective);
                        collective.external_links.ToList().ForEach(el => el.CollectiveId = collective.Id);
                    }
                }
            }

            var collectives = tagsImport.items.Where(i => i.collectives != null).SelectMany(i => i.collectives);
            var extLinks = collectives.SelectMany(c => c.external_links);

            _context.ExternalLinks.AddRange(extLinks);
            _context.Collectives.AddRange(collectives);
            _context.Tags.AddRange(tagsImport.items);
            _context.TagsImports.Add(tagsImport);

            UpdateTagsShares(tagsPopulation);

        }

        private void UpdateTagsShares(int tagsPopulation)
        {
            foreach (var tag in _context.Tags)
            {
                tag.share = ((decimal)tag.count / (decimal)tagsPopulation) * 100;
            }
        }
    }
}
