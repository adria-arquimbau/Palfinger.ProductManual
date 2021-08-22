using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using LanguageExt;
using NSubstitute;
using Palfinger.ProductManual.Domain;
using Palfinger.ProductManual.Domain.Helpers;
using Palfinger.ProductManual.Domain.Repositories;
using Palfinger.ProductManual.Queries.Exceptions;
using Palfinger.ProductManual.Queries.Handlers;
using Palfinger.ProductManual.Queries.Models;
using Xunit;
using Attribute = Palfinger.ProductManual.Domain.Attribute;

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
            var attribute1 = new Attribute("Attribute Name 1", "Description", "ImageUrl");
            attribute1.SetConfiguration(new Configuration("Configuration Name 1", "Description", "ImageUrl"));
            var attribute2 = new Attribute("Attribute Name 2", "Description", "ImageUrl");
            attribute2.SetConfiguration(new Configuration("Configuration Name 2", "Description", "ImageUrl"));
            
            var attributesList = new List<Attribute>
            {
                attribute1,
                attribute2
            };

            var pagedList = new PagedList<Attribute>(attributesList, 2, 1, 3);
            _repositoryWrapper.ProductRepository.FindByCondition(Arg.Any<Expression<Func<Product, bool>>>()).Returns(new List<Product>{new Product("Name", "Description", "ImageUrl")});
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
                   TotalPages = 1,
                   PageSize = 3,
                   ProductId = 1,
                   TotalCount = 2,
                   Name = "Name",
                   Description = "Description",
                   ImageUrl = "ImageUrl",
                   Attributes = new List<AttributeResponse>
                   {
                        new AttributeResponse
                        {
                            Id = 1,
                            Name = "Attribute Name 1",
                            Description = "Description",
                            ImageUrl = "ImageUrl",
                            Configurations = new List<ConfigurationResponse>
                            {
                                new ConfigurationResponse
                                {
                                    Id = 1,
                                    Description = "Description",
                                    ImageUrl = "ImageUrl",
                                    Name = "Configuration Name 1"
                                }
                            }
                        },
                        new AttributeResponse
                        {
                            Id = 2,
                            Name = "Attribute Name 2",
                            Description = "Description",
                            ImageUrl = "ImageUrl",
                            Configurations = new List<ConfigurationResponse>
                            {
                                new ConfigurationResponse
                                {
                                    Id = 2,
                                    Description = "Description",
                                    ImageUrl = "ImageUrl",
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
            _repositoryWrapper.ProductRepository.FindByCondition(Arg.Any<Expression<Func<Product, bool>>>()).Returns(new List<Product>{new Product("Name", "Description", "ImageUrl")});
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
                   TotalPages = 0,
                   PageSize = 0,
                   ProductId = 1,
                   TotalCount = 0,
                   Name = "Name",
                   Description = "Description",
                   ImageUrl = "ImageUrl",
                   Attributes = new List<AttributeResponse>()
                }
            };
            
            response.Should().BeEquivalentTo(expectedResponse, config => config
                .Excluding(x => x.SelectedMemberPath.EndsWith("Id")));
        }
        
        [Fact]
        public async Task GetACompletePagingWith5RequestsForTheSecondPageOnAProductWith10AttributesAndMultipleConfigurations()
        {
            var attribute1 = new Attribute("Attribute Name 1", "Description", "ImageUrl");
            attribute1.SetConfiguration(new Configuration("Configuration Name 11", "Description", "ImageUrl"));
            var attribute2 = new Attribute("Attribute Name 2", "Description", "ImageUrl");
            attribute2.SetConfiguration(new Configuration("Configuration Name 21", "Description", "ImageUrl")); 
            attribute2.SetConfiguration(new Configuration("Configuration Name 22", "Description", "ImageUrl"));
            attribute2.SetConfiguration(new Configuration("Configuration Name 23", "Description", "ImageUrl"));
            var attribute3 = new Attribute("Attribute Name 3", "Description", "ImageUrl");
            attribute3.SetConfiguration(new Configuration("Configuration Name 31", "Description", "ImageUrl"));
            attribute3.SetConfiguration(new Configuration("Configuration Name 32", "Description", "ImageUrl"));
            var attribute4 = new Attribute("Attribute Name 4", "Description", "ImageUrl");
            attribute4.SetConfiguration(new Configuration("Configuration Name 41", "Description", "ImageUrl"));
            attribute4.SetConfiguration(new Configuration("Configuration Name 42", "Description", "ImageUrl"));
            attribute4.SetConfiguration(new Configuration("Configuration Name 43", "Description", "ImageUrl"));
            var attribute5 = new Attribute("Attribute Name 5", "Description", "ImageUrl");
            attribute5.SetConfiguration(new Configuration("Configuration Name 51", "Description", "ImageUrl"));
            
            var attributesList = new List<Attribute>    
            {
                attribute1,
                attribute2,
                attribute3,
                attribute4,
                attribute5
            };

            const int productId = 1;

            var pagedList = new PagedList<Attribute>(attributesList, 10, 2, 5);
            
            _repositoryWrapper.ProductRepository.FindByCondition(Arg.Any<Expression<Func<Product, bool>>>()).Returns(new List<Product>{new Product("Name", "Description", "ImageUrl")});
            _repositoryWrapper.AttributeRepository.GetAttributesPaging(Arg.Any<ManualByProductIdFilterRequest>()).Returns(pagedList);

            var request = new GetManualByProductIdQueryRequest(productId,2,5);
            var response = await _handler.Handle(request, CancellationToken.None);

            var expectedResponse = new GetManualByProductIdQueryResponse
            {
                ManualByProductIdPagingResponse = new ManualByProductIdPagingResponse
                {
                   CurrentPage = 2,
                   HasNext = false,
                   HasPrevious = true,
                   TotalPages = 2,
                   PageSize = 5,
                   ProductId = 1,
                   TotalCount = 10,
                   Description = "Description",
                   ImageUrl = "ImageUrl",
                   Name = "Name",
                   Attributes = new List<AttributeResponse>
                   {
                        new AttributeResponse
                        {
                            Id = 1,
                            Name = "Attribute Name 1",
                            Description = "Description",
                            ImageUrl = "ImageUrl",
                            Configurations = new List<ConfigurationResponse>
                            {
                                new ConfigurationResponse
                                {
                                    Id = 1,
                                    Description = "Description",
                                    ImageUrl = "ImageUrl",
                                    Name = "Configuration Name 11"
                                }
                            }
                        },
                        new AttributeResponse
                        {
                            Id = 2,
                            Name = "Attribute Name 2",
                            Description = "Description",
                            ImageUrl = "ImageUrl",
                            Configurations = new List<ConfigurationResponse>
                            {
                                new ConfigurationResponse
                                {
                                    Id = 21,
                                    Description = "Description",
                                    ImageUrl = "ImageUrl",
                                    Name = "Configuration Name 21"
                                },
                                new ConfigurationResponse
                                {
                                    Id = 22,
                                    Description = "Description",
                                    ImageUrl = "ImageUrl",
                                    Name = "Configuration Name 22"
                                },
                                new ConfigurationResponse
                                {
                                    Id = 23,
                                    Description = "Description",
                                    ImageUrl = "ImageUrl",
                                    Name = "Configuration Name 23"
                                }
                            }
                        },
                        new AttributeResponse
                        {
                            Id = 3,
                            Name = "Attribute Name 3",
                            Description = "Description",
                            ImageUrl = "ImageUrl",
                            Configurations = new List<ConfigurationResponse>
                            {
                                new ConfigurationResponse
                                {
                                    Id = 31,
                                    Description = "Description",
                                    ImageUrl = "ImageUrl",
                                    Name = "Configuration Name 31"
                                },
                                new ConfigurationResponse
                                {
                                    Id = 32,
                                    Description = "Description",
                                    ImageUrl = "ImageUrl",
                                    Name = "Configuration Name 32"
                                }
                            }
                        },
                        new AttributeResponse
                        {
                            Id = 4,
                            Name = "Attribute Name 4",
                            Description = "Description",
                            ImageUrl = "ImageUrl",
                            Configurations = new List<ConfigurationResponse>
                            {
                                new ConfigurationResponse
                                {
                                    Id = 41,
                                    Description = "Description",
                                    ImageUrl = "ImageUrl",
                                    Name = "Configuration Name 41"
                                },
                                new ConfigurationResponse
                                {
                                    Id = 42,
                                    Description = "Description",
                                    ImageUrl = "ImageUrl",
                                    Name = "Configuration Name 42"
                                },
                                new ConfigurationResponse
                                {
                                    Id = 43,
                                    Description = "Description",
                                    ImageUrl = "ImageUrl",
                                    Name = "Configuration Name 43"
                                },
                            }
                        },
                        new AttributeResponse
                        {
                            Id = 5,
                            Name = "Attribute Name 5",
                            Description = "Description",
                            ImageUrl = "ImageUrl",
                            Configurations = new List<ConfigurationResponse>
                            {
                                new ConfigurationResponse
                                {
                                    Id = 51,
                                    Description = "Description",
                                    ImageUrl = "ImageUrl",
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
        
        [Fact]
        public async Task ReturnAnErrorIfTheProductDoesNotExists()
        {
            const int pageNumber = 1;
            const int notExistingProduct = 1001;
            
            _repositoryWrapper.ProductRepository.FindByCondition(product => product.Id == notExistingProduct).Returns(Option<List<Product>>.None);
            
            var request = new GetManualByProductIdQueryRequest(notExistingProduct,pageNumber,3);
            Func<Task<GetManualByProductIdQueryResponse>> action = () => _handler.Handle(request, CancellationToken.None);

            await action.Should().ThrowAsync<ProductNotFoundException>();
        }
    }
}       