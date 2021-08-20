using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Palfinger.ProductManual.Domain;
using Palfinger.ProductManual.Domain.Repositories;

namespace Palfinger.ProductManual.Infrastructure.Data.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly ProductManualDbContext _context;

        public ProductRepository(ProductManualDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProducts(ManualFilterRequest manualFilterRequest)
        {
            return FindALl()
                .Where(product => product.Id == manualFilterRequest.ProductId)
                .Include(product => product.Attributes
                    .OrderBy(attribute => attribute.Name)
                    .Skip((manualFilterRequest.PageNumber - 1) * manualFilterRequest.PageSize)
                    .Take(manualFilterRequest.PageSize))
                .ToList();
        }
    }
}   