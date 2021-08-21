using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Palfinger.ProductManual.Domain;
using Palfinger.ProductManual.Domain.Repositories;
using Palfinger.ProductManual.Infrastructure.Data.Repositories;
using Xunit;

namespace Palfinger.ProductManual.Tests.Infrastructure.Repositories
{
    [Collection("IntegrationTests")]
    public class ProductRepositoryShould
    {
        private readonly TestContext _testContext;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public ProductRepositoryShould()
        {
            _testContext = new TestContext();
            _repositoryWrapper = new RepositoryWrapper(_testContext.TestDbContext());
            Task.WaitAll(_testContext.TestDbContext().Database.EnsureDeletedAsync());
            Task.WaitAll(_testContext.TestDbContext().Database.EnsureCreatedAsync());
        }

        [Fact]
        public async Task SaveAProduct()
        {
            var product = new Product("Name", "Description", "ImageUrl");
            await _repositoryWrapper.ProductRepository.Create(product);
            await _repositoryWrapper.Save();
            
            var result = await _testContext.TestDbContext().Product
                .FirstOrDefaultAsync();
            
            result.Should().BeEquivalentTo(product);
        }
    }
}   