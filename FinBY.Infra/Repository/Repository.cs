using Microsoft.EntityFrameworkCore;
using FinBY.Domain.Data;
using FinBY.Domain.Entities;
using FinBY.Domain.Repositories;
using FinBY.Infra.Context;
using FinBY.Infra.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FinBY.Infra.Repository
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : Entity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dataset;

        public Repository(DbContext context)
        {
            _context = context;
            _dataset = _context.Set<TEntity>();
        }

        public virtual void Add(TEntity obj)
        {
            _dataset.Add(obj);
        }

        public virtual TEntity GetById(TKey id)
        {
            return _dataset.Find(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _dataset;
        }

        public virtual void Update(TEntity obj)
        {
            _dataset.Update(obj);
        }

        public virtual void Remove(TKey id)
        {
            _dataset.Remove(_dataset.Find(id));
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<bool> DeleteAsync(TKey id)
        {
            var result = await GetByIdAsync(id);
            if (result != null)
            {
                _dataset.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<TEntity> AddAsync(TEntity item)
        {
            _dataset.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {

            return await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<IEnumerable<TEntity>> GetAlltAsync()
        {
            return await _dataset.ToListAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity item)
        {
            var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));

            if (result == null)
                return null;

            _context.Entry(result).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dataset.Where(expression).ToListAsync();
        }


        public PagedResult<TEntity> GetPaged(int page, int pageSize)
        {
            return _dataset.GetPaged<TEntity>(page, pageSize);
        }
    }
}