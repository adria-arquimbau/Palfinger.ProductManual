using System.Linq;
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

        public PagedList<Attribute> GetAttributesPaging(AttributesFromProductFilterRequest attributesFromProductFilterRequest)
        {   
            return PagedList<Attribute>.ToPagedList(FindALl()
                    .Where(attribute => attribute.Product.Id == attributesFromProductFilterRequest.ProductId)
                    .OrderBy(attribute => attribute.Name)
                    .Include(attribute => attribute.Configurations), attributesFromProductFilterRequest.PageNumber, attributesFromProductFilterRequest.PageSize);
        }
    }
}   