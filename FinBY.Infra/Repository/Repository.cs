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

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _dataset.AsNoTracking().SingleOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dataset.AsNoTracking().ToListAsync();
        }    
        
        public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dataset.AsNoTracking().Where(expression).ToListAsync();
        }

        public async Task<PagedResult<TEntity>> GetPagedAsync(int page, int pageSize)
        {
            return await _dataset.AsNoTracking().GetPagedAsync<TEntity>(page, pageSize);
        }   
    }
}