using MediatR;

namespace Palfinger.ProductManual.Queries.Handlers
{
    public class GetManualByProductIdQueryRequest : IRequest<GetManualByProductIdQueryResponse>
    {
        public readonly int RequestProductId;
        public readonly int RequestPageNumber;
        public readonly int RequestPageSize;

        public GetManualByProductIdQueryRequest(int requestProductId, int requestPageNumber, int requestPageSize)
        {
            RequestProductId = requestProductId;
            RequestPageNumber = requestPageNumber;
            RequestPageSize = requestPageSize;
        }
    }
}       