using Microsoft.EntityFrameworkCore;
using Palfinger.ProductManual.Domain;

namespace Palfinger.ProductManual.Infrastructure.Data
{
    public class ProductManualDbContext : DbContext
    {
        public ProductManualDbContext(DbContextOptions<ProductManualDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Product { get; set; }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
        }
    }
}