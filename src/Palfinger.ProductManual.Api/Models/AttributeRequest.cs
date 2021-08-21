using System.Collections.Generic;

namespace Palfinger.ProductManual.Api.Models
{
    public class AttributeRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<ConfigurationsRequest> Configurations { get; set; }
    }
}