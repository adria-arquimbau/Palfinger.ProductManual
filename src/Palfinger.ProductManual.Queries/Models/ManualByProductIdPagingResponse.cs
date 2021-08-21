using System.Collections.Generic;

namespace Palfinger.ProductManual.Queries.Models
{
    public class ManualByProductIdPagingResponse    
    {
        public int ProductId { get; set; }
        public int TotalCount { get; set; } 
        public int PageSize { get; set; }       
        public int CurrentPage { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<AttributeResponse> Attributes { get; set; }
    }
}