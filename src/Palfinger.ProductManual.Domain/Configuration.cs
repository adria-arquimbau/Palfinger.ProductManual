using System;

namespace Palfinger.ProductManual.Domain
{
    public class Configuration
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public Attribute Attribute { get; private set; }
        
        public Configuration(string name)
        {
            Name = name;
        }
    }
}