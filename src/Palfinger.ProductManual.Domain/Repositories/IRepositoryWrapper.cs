using System.Threading.Tasks;

namespace Palfinger.ProductManual.Domain.Repositories
{
    public interface IRepositoryWrapper
    {
        IAttributeRepository AttributeRepository { get;  }
        IProductRepository ProductRepository { get; }
        Task Save();
    }
}       