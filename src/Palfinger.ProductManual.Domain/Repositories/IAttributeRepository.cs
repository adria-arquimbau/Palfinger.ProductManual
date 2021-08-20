using Palfinger.ProductManual.Domain.Helpers;

namespace Palfinger.ProductManual.Domain.Repositories
{
    public interface IAttributeRepository : IRepositoryBase<Attribute>
    {
        PagedList<Attribute> GetAttributesPaging(AttributesFromProductFilterRequest attributesFromProductFilterRequest);
    }
}           