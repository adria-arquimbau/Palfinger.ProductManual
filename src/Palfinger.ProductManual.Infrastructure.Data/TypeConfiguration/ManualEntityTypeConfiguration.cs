using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Palfinger.ProductManual.Domain;

namespace Palfinger.ProductManual.Infrastructure.Data.TypeConfiguration
{
    public class ManualEntityTypeConfiguration : IEntityTypeConfiguration<Manual>
    {
        public void Configure(EntityTypeBuilder<Manual> builder)
        {
            builder.HasKey(manual => manual.Id);
            builder.Property(manual => manual.Name);
            builder.HasMany(manual => manual.Products);
        }
    }
}