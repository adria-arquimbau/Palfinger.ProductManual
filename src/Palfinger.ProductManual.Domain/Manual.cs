using System;
using System.Collections.Generic;

namespace Palfinger.ProductManual.Domain
{
    public class Manual
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public List<Product> Products { get; private set; }
    }
}