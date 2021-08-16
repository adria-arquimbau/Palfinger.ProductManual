using System;

namespace Palfinger.ProductManual.Domain
{
    public class Product
    {
        public Guid Id { get; private set; }    
        public string Name { get; private set; }
    }
}