namespace Palfinger.ProductManual.Domain.Repositories
{
    public interface IRepositoryWrapper
    {
        IProductRepository ProductRepository { get;  }
        void Save();
    }
}