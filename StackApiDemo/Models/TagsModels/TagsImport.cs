using System.Text.Json.Serialization;

namespace StackApiDemo.Models.TagsModels
{
    public class TagsImport
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public ICollection<Tag> items { get; set; }
        public bool has_more { get; set; }
        public int quota_max { get; set; }
        public int quota_remaining { get; set; }
    }
}
