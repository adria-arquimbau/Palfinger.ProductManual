using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Palfinger.ProductManual.Api;
using Palfinger.ProductManual.Domain;
using Palfinger.ProductManual.Tests.Api.SeedData;
using Xbehave;
using Xunit;

namespace Palfinger.ProductManual.Tests.Api.Controllers
{
    [Collection("IntegrationTests")]
    public class ManualsControllerShould : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private HttpResponseMessage _clientResponse;

        public ManualsControllerShould(CustomWebApplicationFactory<Startup> factory)
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
        public void asdad()
        {
            var services = new ServiceCollection();
            var client = CreateClient(services);

            "Given we have a Bike manual"
                .x(async () =>
                {
                    await _factory.ExecuteDbContextAsync(async context =>
                    {
                        await context.Database.ExecuteSqlRawAsync(BikeManual.Script());
                        await context.SaveChangesAsync();
                    });
                });
            
            "When called the method"
                .x(async () =>
                {
                    _clientResponse = await client.GetAsync($"api/manuals");
                });

            "Then we get an ok response"
                .x(() => _clientResponse.StatusCode.Should().Be(HttpStatusCode.OK));
        }

        [Scenario]
        public async Task Test()
        {
            var services = new ServiceCollection();
            var client = CreateClient(services);

            "When called the method"
                .x(async () =>
                {
                    _clientResponse = await client.GetAsync($"api/manuals");
                });

            "Then we get an ok response"
                .x(() => _clientResponse.StatusCode.Should().Be(HttpStatusCode.OK));
            
            Product manualResponse = null; 
            
            await _factory.ExecuteDbContextAsync(async context =>
            {
                //await context.Manual.AddAsync(new Product("Name"));
                await context.SaveChangesAsync();

                manualResponse = await context.Product.FirstOrDefaultAsync();

            });

            manualResponse.Should().BeEquivalentTo(manualResponse);
        }
    }
}           