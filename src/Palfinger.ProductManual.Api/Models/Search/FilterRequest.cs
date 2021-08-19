namespace Palfinger.ProductManual.Api.Models.Search
{
    public abstract class FilterRequest<T>
    {
        public OrderFieldQuery<T>? OrderBy { get; set; }
        public string SearchTerm { get; set; }
        public PaginationRequest? Pagination { get; set; }
    }
}
