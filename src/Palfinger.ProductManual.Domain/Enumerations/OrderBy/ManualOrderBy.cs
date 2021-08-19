namespace Palfinger.ProductManual.Domain.Enumerations.OrderBy
{
    public class ManualOrderBy : Enumeration
    {
        public static ManualOrderBy DEFAULT = new ManualOrderBy(0, "ProductNumber");

        public ManualOrderBy(int id, string name)
            : base(id, name)
        {
        }
    }   
    
}