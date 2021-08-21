using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Palfinger.ProductManual.Domain;

namespace Palfinger.ProductManual.Infrastructure.Data.TypeConfiguration
{
    public class AttributeEntityTypeConfiguration: IEntityTypeConfiguration<Attribute>
    {
        public void Configure(EntityTypeBuilder<Attribute> builder)
        {
            builder.HasKey(attribute => attribute.Id);
            builder.Property(attribute => attribute.Name).IsRequired();
            builder.Property(attribute => attribute.Description).IsRequired();
            builder.Property(attribute => attribute.ImageUrl).IsRequired();
            builder.HasMany(attribute => attribute.Configurations);
        }
    }
}