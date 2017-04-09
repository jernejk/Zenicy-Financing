namespace Zenicy.Financing.Core.Models
{
    public class CategoryDefinition : BaseDefinition
    {
        public CategoryDefinition() { }

        public CategoryDefinition(Category category, string[] includes, string[] excludes)
        {
            Category = category;
            Includes = includes;
            Excludes = excludes;
        }

        public CategoryDefinition(string id, string name, string[] includes, string[] excludes)
            : this(new Category(id, name), includes, excludes)
        {
        }

        public Category Category { get; set; }
    }
}
