using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Palfinger.ProductManual.Domain;

namespace Palfinger.ProductManual.Infrastructure.Data.TypeConfiguration
{
    public class AttributeEntityTypeConfiguration: IEntityTypeConfiguration<Attribute>
    {
        public void Configure(EntityTypeBuilder<Attribute> builder)
        {
            builder.HasKey(product => product.Id);
            builder.Property(product => product.Name);
            builder.HasMany(product => product.Configurations);
        }
    }
}