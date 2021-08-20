using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Palfinger.ProductManual.Domain;
using Palfinger.ProductManual.Domain.Helpers;
using Palfinger.ProductManual.Domain.Repositories;
using Palfinger.ProductManual.Queries.Handlers;
using Palfinger.ProductManual.Queries.Models;
using Palfinger.ProductManual.Tests.Api.SeedData;
using Palfinger.ProductManual.Tests.Infrastructure;
using Xunit;

namespace Palfinger.ProductManual.Tests.QueryHandler
{
    [Collection("IntegrationTests")]
    public class GetManualByProductIdQueryHandlerShould
    {
        private readonly GetManualByProductIdQueryHandler _handler;
        private readonly TestContext _context;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public GetManualByProductIdQueryHandlerShould()
        {
            _repositoryWrapper = Substitute.For<IRepositoryWrapper>();
            _context = new TestContext();
            Task.WaitAll(_context.TestDbContext().Database.EnsureDeletedAsync());
            Task.WaitAll(_context.TestDbContext().Database.EnsureCreatedAsync());
            Task.WaitAll(_context.TestDbContext().Database.ExecuteSqlRawAsync(ProductsManualSeedData.Script()));
            _handler = new GetManualByProductIdQueryHandler(_repositoryWrapper);
        }

        [Fact]
        public async Task GetASimplePagingWith2RequestsForTheFirstPage()
        {
            var attribute1 = new Attribute("Attribute Name 1");
            attribute1.SetConfiguration(new Configuration("Configuration Name 1"));
            var attribute2 = new Attribute("Attribute Name 2");
            attribute2.SetConfiguration(new Configuration("Configuration Name 2"));
            
            var attributesList = new List<Attribute>
            {
                attribute1,
                attribute2
            };

            var pagedList = new PagedList<Attribute>(attributesList, 3, 1, 3);
            _repositoryWrapper.AttributeRepository.GetAttributesPaging(Arg.Any<ManualByProductIdFilterRequest>()).Returns(pagedList);

            var request = new GetManualByProductIdQueryRequest(1,1,3);
            var response = await _handler.Handle(request, CancellationToken.None);

            var expectedResponse = new GetManualByProductIdQueryResponse
            {
                ManualByProductIdPagingResponse = new ManualByProductIdPagingResponse
                {
                   CurrentPage = 1,
                   HasNext = false,
                   HasPrevious = false,
                   PageSize = 3,
                   ProductId = 1,
                   TotalCount = 3,
                    Attributes = new List<AttributeResponse>
                    {
                        new AttributeResponse
                        {
                            Id = 1,
                            Name = "Attribute Name 1",
                            Configurations = new List<ConfigurationResponse>
                            {
                                new ConfigurationResponse
                                {
                                    Id = 1,
                                    Name = "Configuration Name 1"
                                }
                            }
                        },
                        new AttributeResponse
                        {
                            Id = 2,
                            Name = "Attribute Name 2",
                            Configurations = new List<ConfigurationResponse>
                            {
                                new ConfigurationResponse
                                {
                                    Id = 2,
                                    Name = "Configuration Name 2"
                                }
                            }
                        }
                    }
                }
            };
            
            response.Should().BeEquivalentTo(expectedResponse, config => config
                .Excluding(x => x.SelectedMemberPath.EndsWith("Id")));
        }
    }
}