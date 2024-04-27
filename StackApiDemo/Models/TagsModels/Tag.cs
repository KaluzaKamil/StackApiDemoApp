using System.Text.Json.Serialization;

namespace StackApiDemo.Models.TagsModels
{
    public class Tag
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public ICollection<Collective>? collectives { get; set; }
        public bool has_synonyms { get; set; }
        public bool is_moderator_only { get; set; }
        public bool is_required { get; set; }
        public int count { get; set; }
        public string name { get; set; }
        public decimal? share { get; set; }
        [JsonIgnore]
        public TagsImport? TagsImport { get; set; }
        [JsonIgnore]
        public Guid? TagsImportId { get; set; }
    }
}
