using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Palfinger.ProductManual.Queries.Handlers
{
    public class GetManualsQueryHandler : IRequestHandler<GetManualByProductIdQueryRequest, GetManualByProductIdQueryResponse>
    {
        public Task<GetManualByProductIdQueryResponse> Handle(GetManualByProductIdQueryRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}