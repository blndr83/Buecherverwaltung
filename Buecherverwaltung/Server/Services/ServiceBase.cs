using Buecherverwaltung.Server.OrMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Buecherverwaltung.Server.Services
{
    public class ServiceBase<TEntity> : IDisposable where TEntity : Entity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected ServiceBase(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        protected async Task<int> Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            return await _context.SaveChangesAsync(true);
        }

        protected IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().Where(expression);
        }

        protected async Task<int> Update(TEntity entity, TEntity oldEntity)
        {
            _context.Entry(oldEntity).State = EntityState.Detached;
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync(true);
        }

        protected void AddRange(IEnumerable<TEntity> entities)
        {
            try
            {
                _dbSet.AddRange(entities);
                _context.SaveChanges();
            }
            catch (Exception) { }
        }

        protected TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        protected TEntity GetById(string id)
        {
            return _dbSet.Find(id);
        }

        protected async Task<IList<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        protected async Task<int> Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync(true);
        }

        protected void RemoveRange(IEnumerable<TEntity> entities)
        {
            try
            {
                _dbSet.RemoveRange(entities);
                _context.SaveChanges();
            }
            catch (Exception) { }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
