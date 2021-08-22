using System.ComponentModel.DataAnnotations;

namespace Palfinger.ProductManual.Api.Models
{
    public class GetManualByProductIdRequest
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int PageNumber { get; set; }    
        [Required]
        public int PageSize { get; set; }
    }
}