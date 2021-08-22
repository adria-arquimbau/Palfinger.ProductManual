using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Palfinger.ProductManual.Domain.Commands.CreateProduct;
using Palfinger.ProductManual.Domain.Commands.CreateProduct.Models;
using Palfinger.ProductManual.Domain.Repositories;
using Xunit;

namespace Palfinger.ProductManual.Domain.Tests
{
    public class CreateProductCommandHandlerShould
    {
        private readonly IRequestHandler<CreateProductCommandRequest> _handler;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CreateProductCommandHandlerShould()
        {
            _repositoryWrapper = Substitute.For<IRepositoryWrapper>();
            _handler = new CreateProductCommandHandler(_repositoryWrapper);
        }

        [Theory, AutoData]
        public async Task CreateAProductWithoutAttributes(string name, string description, string imageUrl)
        {
            var request = new CreateProductCommandRequest(name, description, imageUrl, new List<CreateAttributeRequest>());
            
            await _handler.Handle(request, CancellationToken.None);

            var product = new Product(name, description, imageUrl);
            await _repositoryWrapper.ProductRepository.Received(1).Create(Arg.Is<Product>(p => IsEquivalentTo(p, product)));
            await _repositoryWrapper.Received(1).Save();
        }
        
        [Fact]
        public async Task CreateAProductWithAttributesAndConfigurations()
        {
            const string name = "";
            const string description = "";
            const string imageUrl = "";
            var request = new CreateProductCommandRequest(name, description, imageUrl, new List<CreateAttributeRequest>
            {
                new CreateAttributeRequest(name, description, imageUrl, new List<CreateConfigurationRequest>
                {
                    new CreateConfigurationRequest(name, description, description)
                })
            });
            
            await _handler.Handle(request, CancellationToken.None);

            var product = new Product(name, description, imageUrl);
            var attribute = new Attribute(name, description, imageUrl);
            var configuration = new Configuration(name, description, imageUrl);
            
            attribute.SetConfigurations(new List<Configuration>
            {
                configuration
            });
            product.SetAttributes(new List<Attribute>
            {
                attribute
            });
            await _repositoryWrapper.ProductRepository.Received(1).Create(Arg.Is<Product>(p => IsEquivalentTo(p, product)));
            await _repositoryWrapper.Received(1).Save();
        }
        
        private bool IsEquivalentTo(object request, object expectedRequest)
        {
            request.Should().BeEquivalentTo(expectedRequest, config => config
                .Excluding(x => x.SelectedMemberPath.EndsWith("Id"))
                .IgnoringCyclicReferences());
            return true; 
        }
    }   
}