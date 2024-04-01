using StackApiDemo.Models.TagsModels;
using StackApiDemo.Parameters;

namespace StackApiDemo.Handlers
{
    public interface IStackOverflowTagsHandler
    {
        IEnumerable<Tag> HandleGetTags(TagParameters tagParameters);
        Task<int> HandleRefreshDatabaseAsync();
    }
}