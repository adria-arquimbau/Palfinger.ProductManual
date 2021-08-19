using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Palfinger.ProductManual.Infrastructure.Data;

namespace Palfinger.ProductManual.Tests
{
    // https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-5.0&viewFallbackFrom=aspnetcore-5
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private readonly IServiceCollection _mockedServices;

        public CustomWebApplicationFactory()
        {
            _mockedServices = new ServiceCollection();
        }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder().UseStartup<TStartup>();
        }
       
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
           builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath);
                config.AddJsonFile(
                    Path.Combine(hostingContext.HostingEnvironment.ContentRootPath,
                        $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json"), false,
                    reloadOnChange: true);
                config.AddEnvironmentVariables();
            });

            builder.ConfigureTestServices(services =>
            {
                foreach (var mockedService in _mockedServices)
                {
                    var mockedType = mockedService.ServiceType;
                    services.RemoveAll(mockedType);
                    services.Add(mockedService);
                }
            });
        }

        public async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
        {
            using (var scope = Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                await action(scope.ServiceProvider);
            }
        }

        public async Task ExecuteDbContextAsync(Func<ProductManualDbContext, Task> action)
        {
            await ExecuteScopeAsync(sp => action(sp.GetService<ProductManualDbContext>()));
        }
    }
}