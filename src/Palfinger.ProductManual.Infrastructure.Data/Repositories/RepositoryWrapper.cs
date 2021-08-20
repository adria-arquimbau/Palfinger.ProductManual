using Palfinger.ProductManual.Domain.Repositories;

namespace Palfinger.ProductManual.Infrastructure.Data.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ProductManualDbContext _context;
        private IProductRepository _productRepository;

        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_context);
                }

                return _productRepository;
            }
        }

        public RepositoryWrapper(ProductManualDbContext context)
        {
            _context = context;
        }
        
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}