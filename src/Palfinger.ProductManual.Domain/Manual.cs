using System;
using System.Collections.Generic;
using Palfinger.ProductManual.Domain.MtoM;

namespace Palfinger.ProductManual.Domain
{
    public class Manual
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public List<ProductConfiguration> ProductConfigurations { get; private set; }
    }
}       