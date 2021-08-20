using System.Collections.Generic;

namespace Palfinger.ProductManual.Queries.Models
{
    public class AttributeResponse  
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ConfigurationResponse> Configurations { get; set; }
    }
}