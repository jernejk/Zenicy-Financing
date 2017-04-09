namespace Zenicy.Financing.Core.Models
{
    public class Tag
    {
        public Tag() { }

        public Tag(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}
