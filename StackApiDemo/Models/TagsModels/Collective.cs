using System.Text.Json.Serialization;

namespace StackApiDemo.Models.TagsModels
{
    public class Collective
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string[] tags { get; set; }
        public ICollection<ExternalLink> external_links { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        [JsonIgnore]
        public Tag? Tag { get; set; }
        [JsonIgnore]
        public Guid? TagId { get; set; }
    }
}
