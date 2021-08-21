using Palfinger.ProductManual.Domain;
using Palfinger.ProductManual.Domain.Repositories;

namespace Palfinger.ProductManual.Infrastructure.Data.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository 
    {
        public ProductRepository(ProductManualDbContext context) : base(context)
        {
        }
    }
}