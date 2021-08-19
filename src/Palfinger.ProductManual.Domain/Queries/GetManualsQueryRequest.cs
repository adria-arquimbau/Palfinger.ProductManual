
using MediatR;

namespace Palfinger.ProductManual.Domain.Queries
{
    public class GetManualsQueryRequest : IRequest<GetManualsQueryResponse>
    {
      
        public GetManualsQueryRequest()
        {
         
        }
    }
}   