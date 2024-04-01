namespace StackApiDemo.Models.TagsModels
{
    public class ExternalLink
    {
        public Guid Id { get; set; }
        public string type { get; set; }
        public string link { get; set; }
        public Collective Collective { get; set; } 
        public Guid CollectiveId { get; set; }
    }
}
