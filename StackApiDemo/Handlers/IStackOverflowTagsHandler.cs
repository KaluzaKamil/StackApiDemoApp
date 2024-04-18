using Microsoft.AspNetCore.Mvc;
using StackApiDemo.Models.TagsModels;
using StackApiDemo.Parameters;

namespace StackApiDemo.Handlers
{
    public interface IStackOverflowTagsHandler
    {
        IEnumerable<Tag> HandleGet(TagParameters tagParameters);
        Tag? HandleGetByName(string name);
        Task<int> HandleRefreshDatabaseAsync();
    }
}