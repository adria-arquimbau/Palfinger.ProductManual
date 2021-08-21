using MediatR;

namespace Palfinger.ProductManual.Queries.Handlers
{
    public class GetManualByProductIdQueryRequest : IRequest<GetManualByProductIdQueryResponse>
    {
        public readonly int ProductId;
        public readonly int PageNumber;
        public readonly int PageSize;

        public GetManualByProductIdQueryRequest(int productId, int pageNumber, int pageSize)
        {
            ProductId = productId;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}       