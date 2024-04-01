using Microsoft.EntityFrameworkCore.Storage;
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
            return _context.Tags
                .OrderByPropertyName(tagParameters.OrderByProperty, tagParameters.OrderByAscending)
                .Skip((tagParameters.PageNumber - 1) * tagParameters.PageSize)
                .Take(tagParameters.PageSize);
        }

        public int AddTagsImports(IEnumerable<TagsImport> tagsImports)
        {
            var tagsPopulation = tagsImports.SelectMany(t => t.items).Sum(i => i.count);
            foreach (var tagsImport in tagsImports)
            {
                var collectives = tagsImport.items.Where(i => i.collectives != null).SelectMany(i => i.collectives);
                var extLinks = collectives.SelectMany(c => c.external_links);
                _context.TagsImports.Add(tagsImport);

                foreach (var tag in tagsImport.items)
                {
                    tag.share = ((decimal)tag.count / (decimal)tagsPopulation) * 100;
                    _context.Tags.Add(tag);
                }

                _context.Collectives.AddRange(collectives);
                _context.ExternalLinks.AddRange(extLinks);
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
