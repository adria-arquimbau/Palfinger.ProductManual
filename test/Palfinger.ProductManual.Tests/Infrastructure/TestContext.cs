using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Palfinger.ProductManual.Infrastructure.Data;

namespace Palfinger.ProductManual.Tests.Infrastructure
{
    public class TestContext
    {
        private readonly ProductManualDbContext _dbContext;

       public TestContext()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(currentDirectory);
            configurationBuilder.AddJsonFile(Path.Combine(currentDirectory, $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json"), false, reloadOnChange: true);
            configurationBuilder.AddEnvironmentVariables();
            var configuration = configurationBuilder.Build();

            var options = new DbContextOptionsBuilder<ProductManualDbContext>()
                .UseSqlite(configuration.GetConnectionString("DBConnection"))
                .Options;
            _dbContext = new ProductManualDbContext(options);
        }

       public ProductManualDbContext TestDbContext()
        {
            return _dbContext; 
        }
    }
}