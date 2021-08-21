using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LanguageExt;

namespace Palfinger.ProductManual.Domain.Repositories
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindALl();
        Task<Option<List<T>>> FindByCondition(Expression<Func<T, bool>> expression);
        Task Create(T entity);    
        void Update(T entity);      
        void Delete(T entity);      
                
    }       
}   