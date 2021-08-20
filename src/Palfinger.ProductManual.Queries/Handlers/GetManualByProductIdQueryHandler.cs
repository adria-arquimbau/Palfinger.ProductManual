using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Palfinger.ProductManual.Domain;
using Palfinger.ProductManual.Domain.Repositories;
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
            var response =  _repositoryWrapper.AttributeRepository.GetAttributesPaging(new ManualByProductIdFilterRequest
            {
                ProductId = request.RequestProductId,
                PageNumber = request.RequestPageNumber,
                PageSize = request.RequestPageSize
            });

            return new GetManualByProductIdQueryResponse
            {
                ManualByProductIdPagingResponse = new ManualByProductIdPagingResponse
                {
                    ProductId = request.RequestProductId,
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
    }
}