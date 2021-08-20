using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Palfinger.ProductManual.Queries.Handlers
{
    public class GetManualsQueryHandler : IRequestHandler<GetManualsQueryRequest, GetManualsQueryResponse>
    {
        public Task<GetManualsQueryResponse> Handle(GetManualsQueryRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}