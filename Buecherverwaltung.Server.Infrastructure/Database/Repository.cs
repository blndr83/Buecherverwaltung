using Buecherverwaltung.Server.Core;
using Buecherverwaltung.Server.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Buecherverwaltung.Server.Infrastructure.Database
{
    internal class Repository : IRepository
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public async Task<int> Add<TEntity>(TEntity entity) where TEntity : Entity
        {
            _context.Set<TEntity>().Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity
        {
            _context.Set<TEntity>().AddRange(entities);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete<TEntity>(TEntity entity) where TEntity : Entity
        {
            _context.Set<TEntity>().Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity
        {
            _context.Set<TEntity>().RemoveRange(entities);
            return await _context.SaveChangesAsync();
        }

        public async Task<TEntity> Find<TEntity>(object id) where TEntity : Entity
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : Entity
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
        }

        public async Task<IReadOnlyList<TEntity>> FindAll<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : Entity
        {
            return await _context.Set<TEntity>().Where(expression).AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAll<TEntity>() where TEntity : Entity
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<int> Update<TEntity>(TEntity entity) where TEntity : Entity
        {
            _context.Set<TEntity>().Update(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
