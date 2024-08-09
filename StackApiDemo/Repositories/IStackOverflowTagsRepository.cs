using Microsoft.EntityFrameworkCore.Storage;
using StackApiDemo.Models.TagsModels;
using StackApiDemo.Parameters;

namespace StackApiDemo.Repositories
{
    public interface IStackOverflowTagsRepository
    {
        Task<int> AddTagsImportsAsync(IEnumerable<TagsImport> tagsImports);
        Task<int> CleanDatabaseAsync();
        Task<IEnumerable<Tag>> GetTagsAsync(TagParameters tagParameters);
        Task<Tag?> GetTagByNameAsync(string queriedName);
        Task<int> DeleteTagAsync(string name);
        Task<int> UpdateTagAsync(Tag tag);
        public Task<IDbContextTransaction> BeginTransactionAsync();
        public Task CommitTransactionAsync();
        public Task RollbackTransactionAsync();
    }
}