namespace Palfinger.ProductManual.Api.Models.Search
{
    public class OrderFieldQuery<T>
    {
        public OrderFieldQueryDirection Direction { get; set; }
        public T OrderBy { get; set; }
    }

    public enum OrderFieldQueryDirection
    {
        ASC = 0,
        DESC = 1,
    }
}
