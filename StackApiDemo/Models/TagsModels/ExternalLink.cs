using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StackApiDemo.Models.TagsModels
{
    public class ExternalLink
    {
        public Guid Id { get; set; }
        public string type { get; set; }
        public string link { get; set; }
        [JsonIgnore]
        public Collective Collective { get; set; }
        [JsonIgnore]
        public Guid CollectiveId { get; set; }
    }
}
