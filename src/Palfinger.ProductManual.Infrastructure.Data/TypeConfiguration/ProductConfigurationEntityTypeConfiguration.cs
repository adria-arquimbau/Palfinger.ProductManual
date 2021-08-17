using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Palfinger.ProductManual.Domain;
using Palfinger.ProductManual.Domain.MtoM;

namespace Palfinger.ProductManual.Infrastructure.Data.TypeConfiguration
{
    public class ProductConfigurationEntityTypeConfiguration: IEntityTypeConfiguration<ProductConfiguration>
    {
        public void Configure(EntityTypeBuilder<ProductConfiguration> builder)
        {
            builder.HasKey(productConfiguration => productConfiguration.Id);
            builder.HasOne(productConfiguration => productConfiguration.Product);
            builder.HasOne(productConfiguration => productConfiguration.Configuration);
        }
    }
}