using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Palfinger.ProductManual.Domain.Commands.CreateProduct.Models;
using Palfinger.ProductManual.Domain.Repositories;

namespace Palfinger.ProductManual.Domain.Commands.CreateProduct
{
    public class CreateProductCommandHandler : AsyncRequestHandler<CreateProductCommandRequest>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CreateProductCommandHandler(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        protected override async Task Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var attributes = new List<Attribute>();
            CreateAttributesWithOwnConfigurations(request.Attributes, attributes);
            var productWithAttributes = SetAttributesToTheNewProduct(request, attributes);

            await _repositoryWrapper.ProductRepository.Create(productWithAttributes);
            await _repositoryWrapper.Save();
        }

        private static Product SetAttributesToTheNewProduct(CreateProductCommandRequest request, List<Attribute> attributes)
        {
            var product = new Product(request.Name, request.Description, request.ImageUrl);
            product.SetAttributes(attributes);
            return product;
        }

        private static void CreateAttributesWithOwnConfigurations(List<CreateAttributeRequest> request, List<Attribute> attributes)
        {
            foreach (var attribute in request)
            {
                var configurations = attribute.Configurations.Select(configuration => 
                    new Configuration(configuration.Name, configuration.Description, configuration.ImageUrl)).ToList();

                var newAttribute = new Attribute(attribute.Name, attribute.Description, attribute.ImageUrl);
                
                newAttribute.SetConfigurations(configurations);
                attributes.Add(newAttribute);
            }   
        }
    }
}   