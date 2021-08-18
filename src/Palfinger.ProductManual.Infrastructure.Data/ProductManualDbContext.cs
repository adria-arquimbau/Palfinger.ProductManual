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
        public DbSet<Attribute> Attribute { get; set; }   
        public DbSet<Configuration> Configuration { get; set; }   
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AttributeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ConfigurationEntityTypeConfiguration());
        }
    }
}