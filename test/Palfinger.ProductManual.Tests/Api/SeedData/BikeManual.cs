using Palfinger.ProductManual.Tests.Api.Extensions;

namespace Palfinger.ProductManual.Tests.Api.SeedData
{
    public class BikeManual
    {
        public static string Script() => AssemblyExtensions.GetManifestResourceAsString("Palfinger.ProductManual.Tests.Api.SeedData.ProductSeedData.sql");
    }
}