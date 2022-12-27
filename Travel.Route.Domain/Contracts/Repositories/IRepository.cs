using System.Linq.Expressions;
using Travel.Route.Domain.Models;

namespace Travel.Route.Domain.Contracts.Repositories
{
    public interface IRepository<TEntity>
           where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        TEntity Update(TEntity entity);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IIncludable<TEntity>, IIncludable> includes = null);

        Task<TEntity> FirstOrDefaultAsync<TField>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, TField>> orderByKeySelector = null, bool orderAscending = true);

        Task<TEntity> FirstOrDefaultOrdered<TField>(Expression<Func<TEntity, TField>> orderByKeySelector = null, bool orderAscending = true, Func<IIncludable<TEntity>, IIncludable> includes = null);

        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IIncludable<TEntity>, IIncludable> includes = null);

        Task<List<TEntity>> GetOrdered<TField>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, TField>> orderByKeySelector = null, bool orderAscending = true, Func<IIncludable<TEntity>, IIncludable> includes = null);

        Task<PaginatedList<TEntity>> GetPaginatedAsync<TField>(int page, int pageSize, Expression<Func<TEntity, TField>> orderByKeySelector = null, Func<IIncludable<TEntity>, IIncludable> includes = null);

        Task<PaginatedList<TEntity>> GetPaginatedAsync<TKey>(Expression<Func<TEntity, bool>> predicate, int page, int pageSize, Expression<Func<TEntity, TKey>> orderByKeySelector = null, Func<IIncludable<TEntity>, IIncludable> includes = null);
    }
}
