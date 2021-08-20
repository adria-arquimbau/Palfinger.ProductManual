using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Palfinger.ProductManual.Domain;
using Palfinger.ProductManual.Domain.Helpers;
using Palfinger.ProductManual.Domain.Repositories;

namespace Palfinger.ProductManual.Infrastructure.Data.Repositories
{
    public class AttributeRepository : RepositoryBase<Attribute>, IAttributeRepository
    {
        public AttributeRepository(ProductManualDbContext context) : base(context)
        {
        }

        public async Task<PagedList<Attribute>> GetAttributesPaging(ManualByProductIdFilterRequest manualByProductIdFilterRequest)
        {   
            return  PagedList<Attribute>.ToPagedList(FindALl()
                    .Where(attribute => attribute.Product.Id == manualByProductIdFilterRequest.ProductId)
                    .OrderBy(attribute => attribute.Name)
                    .Include(attribute => attribute.Configurations), manualByProductIdFilterRequest.PageNumber, manualByProductIdFilterRequest.PageSize);
        }
    }
}       