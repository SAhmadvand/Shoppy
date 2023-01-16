using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shoppy.Domain;

namespace Shoppy.Persistence.EntityFramework
{
    public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : AggregateRoot<TKey>
        where TKey : IEquatable<TKey>
    {
        public BaseRepository(DbContext dbContext)
        {
            DbContext = dbContext;
            Entity = DbContext.Set<TEntity>();
        }

        protected DbContext DbContext { get; }
        protected DbSet<TEntity> Entity { get; }

        public virtual Task InsertAsync(TEntity entity,
            CancellationToken cancellationToken = default)
        {
            return Entity.AddAsync(entity, cancellationToken).AsTask();
        }

        public virtual void Update(TEntity entity)
        {
            Entity.Update(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            Entity.Remove(entity);
        }

        public virtual Task<TEntity> FindByIdAsync(TKey id,
            CancellationToken cancellationToken = default)
        {
            return Entity.FirstAsync(t => t.Id.Equals(id), cancellationToken);
        }

        public Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return Entity.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return Entity.Where(predicate).ToListAsync(cancellationToken);
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return Entity.AnyAsync(predicate, cancellationToken);
        }
    }
}