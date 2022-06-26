using Microsoft.EntityFrameworkCore;
using SproomInbox.Domain.Entities;
using SproomInbox.Domain.Repositories;
using SproomInbox.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SproomInbox.Infra.Repository
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : EntityGuid
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dataset;

        public Repository(DbContext context)
        {
            _context = context;
            if(_context.ChangeTracker != null)
                _context.ChangeTracker.LazyLoadingEnabled = false;
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
            var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
            try
            {
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
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TEntity> InsertAsync(TEntity item)
        {
            try
            {
                _dataset.Add(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return item;
        }

        public async Task<TEntity> SelectAsync(TKey id)
        {
            try
            {
                return await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<TEntity>> SelectAsync()
        {
            try
            {
                return await _dataset.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity item)
        {
            var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));

            if (result == null)
                return null;

            try
            {
                _context.Entry(result).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return item;
        }
    }


}