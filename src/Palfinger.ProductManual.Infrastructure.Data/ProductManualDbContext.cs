using Microsoft.EntityFrameworkCore;
using Palfinger.ProductManual.Domain;
using Palfinger.ProductManual.Infrastructure.Data.TypeConfiguration;

namespace Palfinger.ProductManual.Infrastructure.Data
{
    public class ProductManualDbContext : DbContext
    {
        public ProductManualDbContext(DbContextOptions<ProductManualDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Manual> Manual { get; set; }   
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ManualEntityTypeConfiguration());
        }
    }
}