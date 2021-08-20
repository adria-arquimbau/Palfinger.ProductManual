namespace Palfinger.ProductManual.Domain
{
    public class ManualByProductIdFilterRequest
    {
        public int ProductId { get; set; }
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        private const int MaxPageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}