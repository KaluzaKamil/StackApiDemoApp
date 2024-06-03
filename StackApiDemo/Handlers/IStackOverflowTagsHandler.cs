using Microsoft.AspNetCore.Mvc;
using StackApiDemo.Models.TagsModels;
using StackApiDemo.Parameters;

namespace StackApiDemo.Handlers
{
    public interface IStackOverflowTagsHandler
    {
        Task<IEnumerable<Tag>> HandleGetAsync(TagParameters tagParameters);
        Task<Tag?> HandleGetByNameAsync(string name);
        Task<int> HandleRefreshDatabaseAsync();
        Task<int> HandleAddTagsImportAsync(TagsImport tagsImport);
        Task<int> HandleDeleteTagAsync(string name);
        Task<int> HandleUpdateTagAsync(Tag tag);
    }
}