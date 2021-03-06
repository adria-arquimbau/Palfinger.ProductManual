using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Palfinger.ProductManual.Api;
using Palfinger.ProductManual.Domain;
using Palfinger.ProductManual.Queries.Models;
using Palfinger.ProductManual.Tests.Api.SeedData;
using Xbehave;
using Xunit;

namespace Palfinger.ProductManual.Tests.Api.Controllers
{
    [Collection("IntegrationTests")]
    public class ProductsControllerShould : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private HttpResponseMessage _clientResponse;

        public ProductsControllerShould(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            Task.WaitAll(_factory.ExecuteDbContextAsync(async context =>
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
            }));
            _clientResponse = new HttpResponseMessage(HttpStatusCode.NotImplemented);
        }   
        
        private HttpClient CreateClient(IServiceCollection mockedServices)
        {
            var client = _factory.WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        foreach (var service in mockedServices)
                        {
                            services.Add(service);
                        }
                    });
                })
                .CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });

            return client;
        }
        
        [Scenario]
        public void GetAProductManualWithPagingAttributesByProductId()
        {
            var services = new ServiceCollection(); 
            var client = CreateClient(services);
            
            const int productId = 3;
            const int pageNumber = 1;
            const int pageSize = 1;   
            const string productName = "Skate";
            const string description = "Description";
            const string imageUrl = "https://example.com/image.png";

            "Given we have an environment with Products, Attributes and Configurations"
                .x(async () =>
                {
                    await _factory.ExecuteDbContextAsync(async context =>
                    {
                        await context.Database.ExecuteSqlRawAsync(ProductsManualSeedData.Script());
                        await context.SaveChangesAsync();
                    });
                });
            
            "When called the method"
                .x(async () =>
                {
                    _clientResponse = await client.GetAsync($"api/products?productId={productId}&pageNumber={pageNumber}&pageSize={pageSize}");
                });

            "Then we get an ok response"
                .x(() => _clientResponse.StatusCode.Should().Be(HttpStatusCode.OK));

            var expectedResponse = new 
            {
                CurrentPage = pageNumber,
                HasNext = true,
                HasPrevious = false,
                PageSize = pageSize,
                ProductId = productId,
                TotalCount = 4,
                Name = productName,
                Description = description,
                ImageUrl = imageUrl,
                Attributes = new List<AttributeResponse>
                {
                    new AttributeResponse
                    {
                        Id = 10,
                        Name = "Deck",
                        Description = description,
                        ImageUrl = imageUrl,
                        Configurations = new List<ConfigurationResponse>
                        {
                            new ConfigurationResponse
                            {
                                Id = 10,
                                Description = description,
                                ImageUrl = imageUrl,
                                Name = "Standard"
                            }
                        }
                    }
                }
            };
            
            "And we obtain the desired information"
                .x(async () =>
                {
                    var json = await _clientResponse.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ManualByProductIdPagingResponse>(json);
                    result.Should().BeEquivalentTo(expectedResponse, config => config
                        .Excluding(x => x.SelectedMemberPath.EndsWith("Id")));
                });
        }

        [Scenario]
        public void CreateAProductWithOneAttributeAndOneConfiguration()
        {
            var services = new ServiceCollection(); 
            var client = CreateClient(services);

            const string productName = "ProductName1";
            const string description = "Description";
            const string imageUrl = "ImageUrl";
            var request = new
            {   
                Name = productName,
                Description = description,
                ImageUrl = imageUrl,
                Attributes = new List<object>
                {
                    new
                    {   
                        Name = "AttributeName1", 
                        Description = description,    
                        ImageUrl = imageUrl,
                        Configurations = new List<object>    
                        {       
                            new    
                            {       
                                Name = "ConfigurationName1", 
                                Description = description,
                                ImageUrl = imageUrl,  
                            }   
                        }   
                    }
                }
            };  
            
            "When called the method"
                .x(async () =>
                {
                    _clientResponse = await client.PostAsync($"api/products", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
                });

            "Then we get an ok response"
                .x(() => _clientResponse.StatusCode.Should().Be(HttpStatusCode.OK));
            
            Product result = null;
            "And we obtain the desired information"
                .x(async () =>
                {
                    await _factory.ExecuteDbContextAsync(async context =>
                        result = await context.Product
                            .Where(p => p.Name == productName)
                            .Include(p => p.Attributes)
                                .ThenInclude(a => a.Configurations)
                            .FirstOrDefaultAsync());
                });

            var expected = new Product(productName, "Description", "ImageUrl");
            var attribute = new Attribute("AttributeName1", description, imageUrl); 
            attribute.SetConfigurations(new List<Configuration>{ new Configuration("ConfigurationName1", description, imageUrl)});
            expected.SetAttributes(new List<Attribute> { attribute });
            "Then we match the information"
                .x(() =>
                {
                    result.Should().BeEquivalentTo(expected, config =>
                        config.Excluding(x => x.SelectedMemberPath.EndsWith("Id"))
                            .Excluding(x => x.SelectedMemberPath.EndsWith("Product"))
                            .Excluding(x => x.SelectedMemberPath.EndsWith("Attribute")));
                });
        }
    }
}           