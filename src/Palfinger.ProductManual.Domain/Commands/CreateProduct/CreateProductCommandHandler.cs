using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
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
            var product = new Product(request.Name, request.Description, request.ImageUrl);
            
            var attributes = new List<Attribute>();
            foreach (var attribute in request.Attributes)
            {
                var configurations = new List<Configuration>();
                
                foreach (var configuration in attribute.Configurations)
                {
                    configurations.Add(new Configuration(configuration.Name, configuration.Description, configuration.ImageUrl));        
                }
                var newAttribute = new Attribute(attribute.Name, attribute.Description, attribute.ImageUrl);
                newAttribute.SetConfigurations(configurations);
                attributes.Add(newAttribute);
            }
            
            product.SetAttributes(attributes);

            await _repositoryWrapper.ProductRepository.Create(product);
        }
    }
}   