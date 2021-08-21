using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Palfinger.ProductManual.Domain;

namespace Palfinger.ProductManual.Infrastructure.Data.TypeConfiguration
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(product => product.Id);
            builder.Property(product => product.Name).IsRequired();
            builder.Property(product => product.Description).IsRequired();
            builder.Property(product => product.ImageUrl).IsRequired();
            builder.HasMany(product => product.Attributes);
        }
    }
}