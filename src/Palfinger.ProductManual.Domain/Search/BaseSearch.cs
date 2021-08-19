using Palfinger.ProductManual.Domain.Enumerations;
using LanguageExt;

namespace Palfinger.ProductManual.Domain.Search
{
    public abstract class BaseSearch<T> where T : Enumeration
    {
        public Option<OrderByResult<T>> OrderBy;

        public Option<Pagination> Pagination;

        public Option<string> SearchTerm;

        public BaseSearch(Option<OrderByResult<T>> orderBy, Option<Pagination> pagination, Option<string> searchTerm)
        {
            OrderBy = orderBy;
            Pagination = pagination;
            SearchTerm = searchTerm;
        }

    }
}
