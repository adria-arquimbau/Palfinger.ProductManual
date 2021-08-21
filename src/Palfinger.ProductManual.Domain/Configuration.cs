namespace Palfinger.ProductManual.Domain
{
    public class Configuration
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; } 
        public string ImageUrl { get; private set; }
        public Attribute Attribute { get; private set; }

        public Configuration(string name, string description, string imageUrl)
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
        }
    }
}