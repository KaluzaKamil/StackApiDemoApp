using StackApiDemo.Models.TagsModels;

namespace StackApiDemo.Models.ViewModels
{
    public class TagViewModel
    {
        public Guid Id { get; set; }
        public ICollection<CollectiveViewModel>? collectives { get; set; }
        public bool has_synonyms { get; set; }
        public bool is_moderator_only { get; set; }
        public bool is_required { get; set; }
        public int count { get; set; }
        public string name { get; set; }
        public decimal share { get; set; }
    }
}
