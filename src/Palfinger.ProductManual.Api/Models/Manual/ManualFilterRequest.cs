using LanguageExt;
using Palfinger.ProductManual.Api.Models.Enumerations;
using Palfinger.ProductManual.Api.Models.Search;
using Palfinger.ProductManual.Domain.Enumerations;
using Palfinger.ProductManual.Domain.Enumerations.OrderBy;
using Palfinger.ProductManual.Domain.Queries;
using Palfinger.ProductManual.Domain.Search;

namespace Palfinger.ProductManual.Api.Models.Manual
{
    public class ManualFilterRequest : FilterRequest<FullOrderByRequest>
    {
        public GetManualsQueryRequest ToDomain()
        {
            var pagination = Option<Pagination>.None;
            var orderBy = Option<OrderByResult<ManualOrderBy>>.None;

            if (OrderBy != null)
            {
                var orderByDirection = (OrderByResultDirection)OrderBy.Direction;
                var orderByField = Enumeration.FromValue<ManualOrderBy>((int)OrderBy.OrderBy);
                orderBy = new OrderByResult<ManualOrderBy>(orderByDirection, orderByField);
            }

            if (Pagination != null)
            {
                var pageNumber = Pagination.PageNumber;
                var recordsPerPage = Pagination.RecordsPerPage;
                pagination = new Pagination(pageNumber, recordsPerPage);
            }

            var filter = string.IsNullOrWhiteSpace(SearchTerm) ? Option<string>.None : SearchTerm;

            return new GetManualsQueryRequest(filter, orderBy, pagination);
        }
    }
}