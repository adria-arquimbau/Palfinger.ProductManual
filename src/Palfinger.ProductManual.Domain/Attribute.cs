using System.Collections.Generic;

namespace Palfinger.ProductManual.Domain
{
    public class Attribute
    {
        public int Id { get; private set; } 
        public string Name { get; private set; }
        public Product Product { get; private set; }
        public List<Configuration> Configurations { get; private set; } = new List<Configuration>();
        
        public Attribute(string name)
        {
            Name = name;
        }

        public void SetConfiguration(Configuration configuration)
        {
            Configurations.Add(configuration);  
        }
    }   
}               