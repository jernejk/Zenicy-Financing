namespace Zenicy.Financing.Core.Models
{
    public class Category
    {
        public Category() { }

        public Category(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; set; }

        public string Name { get; set; }
    }
}
