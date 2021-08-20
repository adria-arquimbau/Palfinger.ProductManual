using System.Threading.Tasks;
using Palfinger.ProductManual.Domain.Helpers;

namespace Palfinger.ProductManual.Domain.Repositories
{
    public interface IAttributeRepository : IRepositoryBase<Attribute>
    {
        Task<PagedList<Attribute>> GetAttributesPaging(ManualByProductIdFilterRequest manualByProductIdFilterRequest);
    }
}               