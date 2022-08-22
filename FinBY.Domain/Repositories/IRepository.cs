using FinBY.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Domain.Repositories
{
    public interface IRepository<TEntity, TKey> 
    {
        void Add(TEntity obj);
        void Update(TEntity obj);
        void Remove(TKey id);
        int SaveChanges();
        Task SaveChangesAsync();
        Task<TEntity> GetByIdAsync(TKey id);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression);
        PagedResult<TEntity> GetPaged(int page,  int pageSize);
    }
}
