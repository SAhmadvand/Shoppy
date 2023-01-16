using Shoppy.Domain;
using System.Linq.Expressions;

namespace Shoppy.Persistence
{
    public interface IBaseRepository<TEntity, TKey>
        where TEntity : AggregateRoot<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default);
        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
