using System.Threading.Tasks;
using Palfinger.ProductManual.Domain.Repositories;

namespace Palfinger.ProductManual.Infrastructure.Data.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ProductManualDbContext _context;
        private IAttributeRepository _attributeRepository;
        private IProductRepository _productRepository;
        public IAttributeRepository AttributeRepository => _attributeRepository ??= new AttributeRepository(_context);
        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context);

        public RepositoryWrapper(ProductManualDbContext context)
        {
            _context = context;
        }
        
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}   