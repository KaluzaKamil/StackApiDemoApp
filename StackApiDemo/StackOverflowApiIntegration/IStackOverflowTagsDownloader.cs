using StackApiDemo.Models.TagsModels;

namespace StackApiDemo.StackOverflowApiIntegration
{
    public interface IStackOverflowTagsDownloader
    {
        Task<List<TagsImport>> ImportStackOverflowTagsAsync();
    }
}