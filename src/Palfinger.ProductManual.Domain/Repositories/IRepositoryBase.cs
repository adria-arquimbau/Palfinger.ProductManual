using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Palfinger.ProductManual.Domain.Repositories
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindALl();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task Create(T entity);    
        void Update(T entity);    
        void Delete(T entity);    
                
    }   
}