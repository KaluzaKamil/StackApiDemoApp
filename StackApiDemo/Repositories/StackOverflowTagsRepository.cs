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

        public StackOverflowTagsRepository(StackOverflowTagsContext context)
        {
            _context = context;
        }

        public IEnumerable<Tag> GetTags(TagParameters tagParameters)
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

        public IEnumerable<Collective> GetCollectivesByTagId(Guid TagId) 
        {
            return _context.Collectives.Where(c => c.TagId == TagId);
        }

        public IEnumerable<ExternalLink> GetExternalLinksByCollectiveId(Guid CollectiveId) 
        {
            return _context.ExternalLinks.Where(el => el.CollectiveId == CollectiveId);
        }

        public int AddTagsImports(IEnumerable<TagsImport> tagsImports)
        {
            var tagsPopulation = tagsImports.SelectMany(t => t.items).Sum(i => i.count);
            foreach (var tagsImport in tagsImports)
            {
                foreach (var tag in tagsImport.items)
                {
                    tag.share = ((decimal)tag.count / (decimal)tagsPopulation) * 100;
                    tag.TagsImport = tagsImport;

                    if (tag.collectives != null)
                    {
                        foreach (var collective in tag.collectives)
                        {
                            collective.Tag = tag;
                            collective.external_links.ToList().ForEach(el => el.Collective = collective);
                        }
                    }
                }

                var collectives = tagsImport.items.Where(i => i.collectives != null).SelectMany(i => i.collectives);
                var extLinks = collectives.SelectMany(c => c.external_links);
               
                _context.ExternalLinks.AddRange(extLinks);
                _context.Collectives.AddRange(collectives);
                _context.Tags.AddRange(tagsImport.items);
                _context.TagsImports.Add(tagsImport);

            }

            return _context.SaveChanges();
        }

        public int CleanDatabase()
        {
            _context.ExternalLinks.RemoveRange(_context.ExternalLinks);
            _context.Collectives.RemoveRange(_context.Collectives);
            _context.Tags.RemoveRange(_context.Tags);

            return _context.SaveChanges();
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
    }
}
