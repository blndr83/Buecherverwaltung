using Buecherverwaltung.Server.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Buecherverwaltung.Server.Core
{
    public interface IRepository
    {
        Task<int> Add<TEntity>(TEntity entity) where TEntity : Entity;
        Task<int> AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity;
        Task<int> Update<TEntity>(TEntity entity) where TEntity : Entity;
        Task<int> Delete<TEntity>(TEntity entity) where TEntity : Entity;
        Task<int> DeleteRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity;
        Task<TEntity> Find<TEntity>(object id) where TEntity : Entity;
        Task<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : Entity;
        Task<IReadOnlyList<TEntity>> FindAll<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : Entity;
        Task<IReadOnlyList<TEntity>> GetAll<TEntity>() where TEntity : Entity;
        void Migrate();
    }
}
