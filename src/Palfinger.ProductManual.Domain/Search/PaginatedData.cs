using System.Collections.Generic;

namespace Palfinger.ProductManual.Domain.Search
{
    public class PaginatedData<T>
    {
        public long TotalRecords;
        public IList<T> Results;

        public PaginatedData(IList<T> items, long totalRecords)
        {
            TotalRecords = totalRecords;
            Results = items;
        }
    }
}
