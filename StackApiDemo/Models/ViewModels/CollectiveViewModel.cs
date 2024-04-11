using StackApiDemo.Models.TagsModels;

namespace StackApiDemo.Models.ViewModels
{
    public class CollectiveViewModel
    {
        public Guid Id { get; set; }
        public string[] tags { get; set; }
        public ICollection<ExternalLinkViewModel> external_links { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
    }
}
