namespace Palfinger.ProductManual.Domain.Search
{
    public class OrderByResult<T>
    {
        public OrderByResultDirection Direction;

        public T OrderBy;

        public OrderByResult(OrderByResultDirection direction, T orderBy)
        {
            Direction = direction;
            OrderBy = orderBy;
        }
    }

    public enum OrderByResultDirection
    {
        ASC = 0,
        DESC = 1,
    }
}
