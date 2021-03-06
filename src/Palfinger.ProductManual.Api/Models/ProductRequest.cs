using System.Collections.Generic;

namespace Palfinger.ProductManual.Api.Models
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<AttributeRequest> Attributes { get; set; }
    }
}