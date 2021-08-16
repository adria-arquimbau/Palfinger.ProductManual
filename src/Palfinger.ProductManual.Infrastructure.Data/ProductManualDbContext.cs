using Microsoft.EntityFrameworkCore;
using Palfinger.ProductManual.Domain;

namespace Palfinger.ProductManual.Infrastructure.Data
{
    public class ProductManualDbContext : DbContext
    {
        public ProductManualDbContext(DbContextOptions<ProductManualDbContext> contextOptions) : base(contextOptions)
        {
        }
        
        public const string DEFAULT_SCHEMA = "PM";
        public DbSet<Product> Users { get; set; }
    }   
}