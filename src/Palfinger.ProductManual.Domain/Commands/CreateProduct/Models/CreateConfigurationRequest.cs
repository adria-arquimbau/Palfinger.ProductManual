namespace Palfinger.ProductManual.Domain.Commands.CreateProduct.Models
{
    public class CreateConfigurationRequest
    {
        public string Name { get; }
        public string Description { get; }
        public string ImageUrl { get; }

        public CreateConfigurationRequest(string name, string description, string imageUrl)
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
        }
    }
}   