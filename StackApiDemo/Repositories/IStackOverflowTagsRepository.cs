using Microsoft.EntityFrameworkCore.Storage;
using StackApiDemo.Models.TagsModels;
using StackApiDemo.Parameters;

namespace StackApiDemo.Repositories
{
    public interface IStackOverflowTagsRepository
    {
        int AddTagsImports(IEnumerable<TagsImport> tagsImports);
        int CleanDatabase();
        IEnumerable<Tag> GetTags(TagParameters tagParameters);
        Tag? GetTagByName(string queriedName);
        public IDbContextTransaction BeginTransaction();
        public void CommitTransaction();
        public void RollbackTransaction();
    }
}