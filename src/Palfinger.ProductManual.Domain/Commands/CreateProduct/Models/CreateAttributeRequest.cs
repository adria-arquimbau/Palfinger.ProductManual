using System.Collections.Generic;

namespace Palfinger.ProductManual.Domain.Commands.CreateProduct.Models
{
    public class CreateAttributeRequest
    {
        public string Name { get; }
        public string Description { get; }
        public string ImageUrl { get; }
        public List<CreateConfigurationRequest> Configurations { get; }

        public CreateAttributeRequest(string name, string description, string imageUrl, List<CreateConfigurationRequest> configurations)
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            Configurations = configurations;
        }
    }
}