using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Palfinger.ProductManual.Infrastructure.Data;
using Respawn;

namespace Palfinger.ProductManual.Tests
{
    // https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-5.0&viewFallbackFrom=aspnetcore-5
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        private readonly IServiceCollection _mockedServices;

        public CustomWebApplicationFactory()
        {
            _mockedServices = new ServiceCollection();
        }

        public void AddTestService<T>(T testService) where T : class
        {
            _mockedServices.TryAddScoped(s => testService);
        }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder()
                .UseStartup<TStartup>();
        }
        // move to configuration
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            CheckDatabaseIsForUnitTesting();

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



        private static void CheckDatabaseIsForUnitTesting()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(currentDirectory);

            configurationBuilder.AddJsonFile(
                Path.Combine(currentDirectory,
                    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json"), false,
                reloadOnChange: true);
            configurationBuilder.AddEnvironmentVariables();
            var configuration = configurationBuilder.Build();

            var connectionString = configuration.GetConnectionString("DBConnection");
            if (connectionString.Contains("Host=bvvlb-psql-001.voxelgroup.net;") &&
                connectionString.Contains("Database=InvoiceCommonPalladium;"))
            {
                throw new ConnectionAbortedException(
                    "This database is for web testing only. Testing it will reset all its data. Please use localhost or integration");
            }

            if (connectionString.Contains("Host=avvlp-psql-009.voxelgroup.net;"))
            {
                throw new ConnectionAbortedException(
                    "This database is for web testing only. Testing it will reset all its data. Please use localhost or integration");
            }
        }

        public async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
        {
            using (var scope = this.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                await action(scope.ServiceProvider);
            }
        }

        public async Task ExecuteDbContextAsync(Func<ProductManualDbContext, Task> action)
        {
            await ExecuteScopeAsync(sp => action(sp.GetService<ProductManualDbContext>()));
        }

        private readonly Checkpoint _checkpoint = new Checkpoint
        {
            TablesToIgnore = new[] { "__EFMigrationsHistory" }
        };

        public async Task RespawnDbContext()
        {
            await ExecuteDbContextAsync(async context =>
            {
                var con = context.Database.GetDbConnection();

                await con.OpenAsync();
                await _checkpoint.Reset(con);
            });
        }
    }
}