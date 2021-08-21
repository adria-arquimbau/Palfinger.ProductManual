namespace Palfinger.ProductManual.Api.Models
{
    public class GetManualByProductIdRequest
    {
        public int ProductId { get; set; }
        public int PageNumber { get; set; }     
        public int PageSize { get; set; }
    }
}