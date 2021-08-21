using System.Collections.Generic;

namespace Palfinger.ProductManual.Domain
{
    public class Product
    {
        public Product(string name, string description, string imageUrl)
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; } 
        public string ImageUrl { get; private set; }    
        public List<Attribute> Attributes { get; private set; }
    }
}       