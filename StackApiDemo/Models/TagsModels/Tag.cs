namespace StackApiDemo.Models.TagsModels
{
    public class Tag
    {
        public Guid Id { get; set; }
        public List<Collective>? collectives { get; set; }
        public bool has_synonyms { get; set; }
        public bool is_moderator_only { get; set; }
        public bool is_required { get; set; }
        public int count { get; set; }
        public string name { get; set; }
        public decimal share { get; set; }
        public TagsImport TagsImport { get; set; }
        public Guid TagsImportId { get; set; }
    }
}
