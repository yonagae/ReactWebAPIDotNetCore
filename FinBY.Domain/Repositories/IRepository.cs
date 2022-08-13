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
        TEntity GetById(TKey id);
        IQueryable<TEntity> GetAll();
        void Update(TEntity obj);
        void Remove(TKey id);
        int SaveChanges();

        Task<TEntity> InsertAsync(TEntity item);
        Task<TEntity> UpdateAsync(TEntity item);
        Task<bool> DeleteAsync(TKey id);
        Task<TEntity> SelectByIdAsync(TKey id);
        Task<IEnumerable<TEntity>> SelectAsync();
        Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> expression);

        PagedResult<TEntity> GetPaged(int page,  int pageSize);
    }
}
