using LanguageExt;
using MediatR;
using Palfinger.ProductManual.Domain.Enumerations.OrderBy;
using Palfinger.ProductManual.Domain.Search;

namespace Palfinger.ProductManual.Domain.Queries
{
    public class GetManualsQueryRequest : IRequest<GetManualsQueryResponse>
    {
        public readonly Option<string> SearchTerm;

        public Option<OrderByResult<ManualOrderBy>> OrderBy;

        public Option<Pagination> Pagination;
        public GetManualsQueryRequest(Option<string> searchTerm, Option<OrderByResult<ManualOrderBy>> orderBy, Option<Pagination> pagination)
        {
            SearchTerm = searchTerm;
            OrderBy = orderBy;
            Pagination = pagination;
        }
    }
}