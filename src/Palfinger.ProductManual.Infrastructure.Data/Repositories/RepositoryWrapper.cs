using Palfinger.ProductManual.Domain.Repositories;

namespace Palfinger.ProductManual.Infrastructure.Data.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ProductManualDbContext _context;
        private IAttributeRepository _attributeRepository;

        public IAttributeRepository AttributeRepository
        {
            get
            {
                if (_attributeRepository == null)
                {
                    _attributeRepository = new AttributeRepository(_context);
                }

                return _attributeRepository;
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