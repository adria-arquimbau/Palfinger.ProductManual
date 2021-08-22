using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Palfinger.ProductManual.Domain.Repositories;

namespace Palfinger.ProductManual.Infrastructure.Data.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly ProductManualDbContext _context;

        public RepositoryBase(ProductManualDbContext context)
        {
            _context = context; 
        }

        public IQueryable<T> FindAll()  
        {   
            return _context.Set<T>();
        }
    
        public async Task<Option<List<T>>> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }
    
        public async Task Create(T entity)  
        {
            await _context.Set<T>().AddAsync(entity);       
        }

        public void Update(T entity)        
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}