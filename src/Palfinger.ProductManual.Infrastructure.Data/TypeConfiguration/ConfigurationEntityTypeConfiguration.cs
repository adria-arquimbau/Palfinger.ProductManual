using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Palfinger.ProductManual.Domain;

namespace Palfinger.ProductManual.Infrastructure.Data.TypeConfiguration
{
    public class ConfigurationEntityTypeConfiguration: IEntityTypeConfiguration<Configuration>
    {   
        public void Configure(EntityTypeBuilder<Configuration> builder)
        {
            builder.HasKey(configuration => configuration.Id);
            builder.Property(configuration => configuration.Name);
        }
    }
}   