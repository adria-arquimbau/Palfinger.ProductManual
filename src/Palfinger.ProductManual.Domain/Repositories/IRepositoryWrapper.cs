namespace Palfinger.ProductManual.Domain.Repositories
{
    public interface IRepositoryWrapper
    {
        IAttributeRepository AttributeRepository { get;  }
        void Save();
    }
}