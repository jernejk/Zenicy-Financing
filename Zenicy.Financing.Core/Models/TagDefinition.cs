namespace Zenicy.Financing.Core.Models
{
    public class TagDefinition : BaseDefinition
    {
        public TagDefinition() { }

        public TagDefinition(Tag tag, string[] includes, string[] excludes)
        {
            Tag = tag;
            Includes = includes;
            Excludes = excludes;
        }

        public TagDefinition(string id, string name, string[] includes, string[] excludes)
            : this(new Tag(id, name), includes, excludes)
        {
        }

        public Tag Tag { get; set; }
    }
}
