using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Palfinger.ProductManual.Domain;
using Palfinger.ProductManual.Domain.Repositories;
using Palfinger.ProductManual.Queries.Exceptions;
using Palfinger.ProductManual.Queries.Models;

namespace Palfinger.ProductManual.Queries.Handlers
{
    public class GetManualByProductIdQueryHandler : IRequestHandler<GetManualByProductIdQueryRequest, GetManualByProductIdQueryResponse>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public GetManualByProductIdQueryHandler(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
    
        public async Task<GetManualByProductIdQueryResponse> Handle(GetManualByProductIdQueryRequest request, CancellationToken cancellationToken)
        {
            await CheckIfTheProductExists(request.ProductId);

            var response =  await _repositoryWrapper.AttributeRepository.GetAttributesPaging(new ManualByProductIdFilterRequest
            {
                ProductId = request.ProductId,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            });
            
            return new GetManualByProductIdQueryResponse
            {
                ManualByProductIdPagingResponse = new ManualByProductIdPagingResponse
                {
                    ProductId = request.ProductId,
                    TotalCount = response.TotalCount,
                    PageSize = response.PageSize, 
                    CurrentPage = response.CurrentPage,
                    HasNext = response.HasNext, 
                    HasPrevious = response.HasPrevious,
                    Attributes = response.Select(a => new AttributeResponse
                    {
                        Id = a.Id,
                        Name = a.Name,  
                        Configurations = a.Configurations.Select(c => new ConfigurationResponse
                        {
                            Id = c.Id,
                            Name = c.Name
                        }).ToList()
                    }).ToList()
                }   
            };
        }

        private async Task CheckIfTheProductExists(int productId)
        {
            var product = await _repositoryWrapper.ProductRepository.FindByCondition(x => x.Id == productId);
            if (product.IsNone)
            {
                throw new ProductNotFoundException();
            }
        }
    }
}