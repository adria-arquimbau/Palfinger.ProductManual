using System;
using System.Collections.Generic;

namespace Palfinger.ProductManual.Domain
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public List<Attribute> Attributes { get; private set; }
    }
}       