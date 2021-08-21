using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Palfinger.ProductManual.Domain;
using Palfinger.ProductManual.Domain.Helpers;
using Palfinger.ProductManual.Domain.Repositories;
using Palfinger.ProductManual.Queries.Handlers;
using Palfinger.ProductManual.Queries.Models;
using Xunit;

namespace Palfinger.ProductManual.Tests.QueryHandler
{
    public class GetManualByProductIdQueryHandlerShould
    {
        private readonly GetManualByProductIdQueryHandler _handler;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public GetManualByProductIdQueryHandlerShould()
        {
            _repositoryWrapper = Substitute.For<IRepositoryWrapper>();
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

            var pagedList = new PagedList<Attribute>(attributesList, 2, 1, 3);
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
                   TotalCount = 2,
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
        
        [Fact]
        public async Task GetAnEmptyListOfAttributesIfTheProductDontHaveAnyAttribute()
        {
            var pagedList = new PagedList<Attribute>(new List<Attribute>(), 0, 1, 0);
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
                   PageSize = 0,
                   ProductId = 1,
                   TotalCount = 0,
                   Attributes = new List<AttributeResponse>()
                }
            };
            
            response.Should().BeEquivalentTo(expectedResponse, config => config
                .Excluding(x => x.SelectedMemberPath.EndsWith("Id")));
        }
        
        [Fact]
        public async Task GetACompletePagingWith5RequestsForTheSecondPageOnAProductWith10AttributesAndMultipleConfigurations()
        {
            var attribute1 = new Attribute("Attribute Name 1");
            attribute1.SetConfiguration(new Configuration("Configuration Name 11"));
            var attribute2 = new Attribute("Attribute Name 2");
            attribute2.SetConfiguration(new Configuration("Configuration Name 21")); 
            attribute2.SetConfiguration(new Configuration("Configuration Name 22"));
            attribute2.SetConfiguration(new Configuration("Configuration Name 23"));
            var attribute3 = new Attribute("Attribute Name 3");
            attribute3.SetConfiguration(new Configuration("Configuration Name 31"));
            attribute3.SetConfiguration(new Configuration("Configuration Name 32"));
            var attribute4 = new Attribute("Attribute Name 4");
            attribute4.SetConfiguration(new Configuration("Configuration Name 41"));
            attribute4.SetConfiguration(new Configuration("Configuration Name 42"));
            attribute4.SetConfiguration(new Configuration("Configuration Name 43"));
            var attribute5 = new Attribute("Attribute Name 5");
            attribute5.SetConfiguration(new Configuration("Configuration Name 51"));
            
            var attributesList = new List<Attribute>    
            {
                attribute1,
                attribute2,
                attribute3,
                attribute4,
                attribute5
            };

            var pagedList = new PagedList<Attribute>(attributesList, 10, 2, 5);
            _repositoryWrapper.AttributeRepository.GetAttributesPaging(Arg.Any<ManualByProductIdFilterRequest>()).Returns(pagedList);

            var request = new GetManualByProductIdQueryRequest(1,2,5);
            var response = await _handler.Handle(request, CancellationToken.None);

            var expectedResponse = new GetManualByProductIdQueryResponse
            {
                ManualByProductIdPagingResponse = new ManualByProductIdPagingResponse
                {
                   CurrentPage = 2,
                   HasNext = false,
                   HasPrevious = true,
                   PageSize = 5,
                   ProductId = 1,
                   TotalCount = 10,
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
                                    Name = "Configuration Name 11"
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
                                    Id = 21,
                                    Name = "Configuration Name 21"
                                },
                                new ConfigurationResponse
                                {
                                    Id = 22,
                                    Name = "Configuration Name 22"
                                },
                                new ConfigurationResponse
                                {
                                    Id = 23,
                                    Name = "Configuration Name 23"
                                }
                            }
                        },
                        new AttributeResponse
                        {
                            Id = 3,
                            Name = "Attribute Name 3",
                            Configurations = new List<ConfigurationResponse>
                            {
                                new ConfigurationResponse
                                {
                                    Id = 31,
                                    Name = "Configuration Name 31"
                                },
                                new ConfigurationResponse
                                {
                                    Id = 32,
                                    Name = "Configuration Name 32"
                                }
                            }
                        },
                        new AttributeResponse
                        {
                            Id = 4,
                            Name = "Attribute Name 4",
                            Configurations = new List<ConfigurationResponse>
                            {
                                new ConfigurationResponse
                                {
                                    Id = 41,
                                    Name = "Configuration Name 41"
                                },
                                new ConfigurationResponse
                                {
                                    Id = 42,
                                    Name = "Configuration Name 42"
                                },
                                new ConfigurationResponse
                                {
                                    Id = 43,
                                    Name = "Configuration Name 43"
                                },
                            }
                        },
                        new AttributeResponse
                        {
                            Id = 5,
                            Name = "Attribute Name 5",
                            Configurations = new List<ConfigurationResponse>
                            {
                                new ConfigurationResponse
                                {
                                    Id = 51,
                                    Name = "Configuration Name 51"
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