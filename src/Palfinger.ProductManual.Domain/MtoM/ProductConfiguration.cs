using System;

namespace Palfinger.ProductManual.Domain.MtoM
{
    public class ProductConfiguration
    {
        public Guid Id { get; private set; }
        public Product Product { get; private set; }
        public Configuration Configuration { get; private set; }
    }
}