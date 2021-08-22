using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt;
using MediatR;
using Palfinger.ProductManual.Domain;
using Palfinger.ProductManual.Domain.Helpers;
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
            var product = await RetrieveProduct(request.ProductId);
            
            return await product.Match(async p =>
            {
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
                        TotalPages = response.TotalPages,
                        PageSize = response.PageSize, 
                        CurrentPage = response.CurrentPage,
                        HasNext = response.HasNext, 
                        HasPrevious = response.HasPrevious,
                        Name = p.First().Name,
                        Description = p.First().Description,
                        ImageUrl = p.First().ImageUrl,
                        Attributes = response.Select(a => new AttributeResponse
                        {
                            Id = a.Id,
                            Name = a.Name,  
                            Description = a.Description,
                            ImageUrl = a.ImageUrl,
                            Configurations = a.Configurations.Select(c => new ConfigurationResponse
                            {
                                Id = c.Id,
                                Description = a.Description,
                                ImageUrl = a.ImageUrl,
                                Name = c.Name
                            }).ToList()
                        }).ToList()
                    }   
                };
            }, () => throw new ProductNotFoundException());
        }

        private async Task<Option<List<Product>>> RetrieveProduct(int productId)
        {
            return  await _repositoryWrapper.ProductRepository.FindByCondition(x => x.Id == productId);
        }
    }
}