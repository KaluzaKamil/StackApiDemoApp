namespace StackApiDemo.Models.TagsModels
{
    public class Collective
    {
        public Guid Id { get; set; }
        public List<string> tags { get; set; }
        public List<ExternalLink> external_links { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public Tag? Tag { get; set; }
        public Guid? TagId { get; set; }
    }
}
