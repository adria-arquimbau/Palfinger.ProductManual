using System.IO;
using System.Reflection;

namespace Palfinger.ProductManual.Tests.Api.Extensions
{
    public static class AssemblyExtensions
    {
        public static string GetManifestResourceAsString(string resource) =>
            new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resource)).ReadToEnd();
    }
}