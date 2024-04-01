namespace StackApiDemo.Models.TagsModels
{
    public class TagsImport
    {
        public Guid Id { get; set; }
        public List<Tag> items { get; set; }
        public bool has_more { get; set; }
        public int quota_max { get; set; }
        public int quota_remaining { get; set; }
    }
}
