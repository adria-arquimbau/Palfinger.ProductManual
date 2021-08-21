using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Palfinger.ProductManual.Domain.Commands.CreateProduct
{
    public class CreateProductCommandHandler : AsyncRequestHandler<CreateProductCommandRequest>
    {
        protected override Task Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}