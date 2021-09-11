using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Palfinger.ProductManual.Domain;
using Palfinger.ProductManual.Domain.Helpers;
using Palfinger.ProductManual.Domain.Repositories;
using Palfinger.ProductManual.Infrastructure.Data.Repositories;
using Xunit;

namespace Palfinger.ProductManual.Tests.Infrastructure.Repositories
{
    [Collection("IntegrationTests")]
    public class AttributeRepositoryShould
    {
        private readonly TestContext _testContext;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public AttributeRepositoryShould()  
        {
            _testContext = new TestContext();
            _repositoryWrapper = new RepositoryWrapper(_testContext.TestDbContext());
            Task.WaitAll(_testContext.TestDbContext().Database.EnsureDeletedAsync());
            Task.WaitAll(_testContext.TestDbContext().Database.EnsureCreatedAsync());
        }

        [Theory, AutoData]
        public async Task GetSimpleAttributePagingFromFirstPageWithOneAttribute(string name, string description, string imageUrl)
        {
            var product = new Product(name, description, imageUrl); 
            product.SetAttributes(new List<Attribute>{new Attribute(name, description, imageUrl)});
            await _testContext.TestDbContext().Product.AddAsync(product);
            await _testContext.TestDbContext().SaveChangesAsync();
            
            var expectedResponse = new PagedList<Attribute>(new List<Attribute>
            {
                new Attribute(name, description, imageUrl)
            }, 1,1,1);

            var request = new ManualByProductIdFilterRequest
            {   
                PageNumber = 1,
                PageSize = 1,
                ProductId = 1
            };
            var response = await _repositoryWrapper.AttributeRepository.GetAttributesPaging(request);

            response.Should().BeEquivalentTo(expectedResponse, config => 
                    config
                        .Excluding(x => x.Product)
                        .Excluding(x => x.Id));
        }
        
        [Fact]
        public async Task GetComplexAttributePagingFromSecondPageWithTwoAttributes()
        {
            var product = new Product("productName", "productDescription", "productImageUrl"); 
            product.SetAttributes(new List<Attribute>{new Attribute("attributeName1", "attributeDescription1", "attributeImageUrl1")});
            product.SetAttributes(new List<Attribute>{new Attribute("attributeName2", "attributeDescription2", "attributeImageUrl2")});
            product.SetAttributes(new List<Attribute>{new Attribute("attributeName3", "attributeDescription3", "attributeImageUrl3")});
            product.SetAttributes(new List<Attribute>{new Attribute("attributeName4", "attributeDescription4", "attributeImageUrl4")});
            product.SetAttributes(new List<Attribute>{new Attribute("attributeName5", "attributeDescription5", "attributeImageUrl5")});
            product.SetAttributes(new List<Attribute>{new Attribute("attributeName6", "attributeDescription6", "attributeImageUrl6")});
            await _testContext.TestDbContext().Product.AddAsync(product);
            await _testContext.TestDbContext().SaveChangesAsync();  
                
            var expectedResponse = new PagedList<Attribute>(new List<Attribute>
            {
                new Attribute("attributeName3", "attributeDescription3", "attributeImageUrl3"),
                new Attribute("attributeName4", "attributeDescription4", "attributeImageUrl4")
            }, 6,2,2);
            

            var request = new ManualByProductIdFilterRequest    
            {   
                PageNumber = 2,
                PageSize = 2,
                ProductId = 1
            };
            var response = await _repositoryWrapper.AttributeRepository.GetAttributesPaging(request);

            response.Should().BeEquivalentTo(expectedResponse, config => 
                config
                    .Excluding(x => x.Product)
                    .Excluding(x => x.Id));
        }
    }
}