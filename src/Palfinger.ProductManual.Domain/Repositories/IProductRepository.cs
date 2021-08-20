using System.Collections.Generic;
using System.Linq;

namespace Palfinger.ProductManual.Domain.Repositories
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        IEnumerable<Product> GetProductPaging(ManualFilterRequest manualFilterRequest);
    }
}   