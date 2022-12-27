using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Travel.Route.Data.Context;
using Travel.Route.Domain.Contracts.Repositories;
using Travel.Route.Domain.Extensions;
using Travel.Route.Domain.Models;

namespace Travel.Route.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly Lazy<DbSet<TEntity>> _dbSet;

        public Repository(AppDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbSet = new Lazy<DbSet<TEntity>>(() => dbContext.Set<TEntity>());
        }

        /// <inheritdoc/>
        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var added = await _dbSet.Value.AddAsync(entity, cancellationToken);
            return added.Entity;
        }

        /// <inheritdoc/>
        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) => await _dbSet.Value.AddRangeAsync(entities, cancellationToken);

        /// <inheritdoc/>
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await _dbSet.Value.AsQueryable().Where(predicate).AnyAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = FilterAndOrder<object>(predicate, null);

            return await query.CountAsync();
        }
        /// <inheritdoc/>
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IIncludable<TEntity>, IIncludable> includes = null)
        {
            var query = FilterAndOrder<object>(predicate, null, false, includes);
            return await query.FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<TEntity> FirstOrDefaultAsync<TField>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, TField>> orderByKeySelector = null, bool orderAscending = true)
        {
            var query = FilterAndOrder(predicate, orderByKeySelector, orderAscending, null);
            return await query.FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<TEntity> FirstOrDefaultOrdered<TField>(Expression<Func<TEntity, TField>> orderByKeySelector = null, bool orderAscending = true, Func<IIncludable<TEntity>, IIncludable> includes = null)
        {
            var query = FilterAndOrder(null, orderByKeySelector, orderAscending, includes);
            return await query.FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IIncludable<TEntity>, IIncludable> includes = null)
        {
            var query = FilterAndOrder<object>(predicate, null, false, includes);
            return await query.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<List<TEntity>> GetOrdered<TField>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, TField>> orderByKeySelector = null, bool orderAscending = true, Func<IIncludable<TEntity>, IIncludable> includes = null)
        {
            var query = FilterAndOrder(predicate, orderByKeySelector, orderAscending, includes);
            return await query.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<PaginatedList<TEntity>> GetPaginatedAsync<TField>(int page, int pageSize, Expression<Func<TEntity, TField>> orderByKeySelector = null, Func<IIncludable<TEntity>, IIncludable> includes = null)
        {
            return await GetPaginatedAsync(null, page, pageSize, orderByKeySelector, includes);
        }

        /// <inheritdoc/>
        public async Task<PaginatedList<TEntity>> GetPaginatedAsync<TKey>(Expression<Func<TEntity, bool>> predicate, int page, int pageSize, Expression<Func<TEntity, TKey>> orderByKeySelector = null, Func<IIncludable<TEntity>, IIncludable> includes = null)
        {
            var query = FilterAndOrder(predicate, orderByKeySelector, true, includes);

            var items = await query.Skip(pageSize * (page - 1)).Take(pageSize).ToListAsync();
            var total = await query.CountAsync();

            return new PaginatedList<TEntity>(items, total, page, pageSize);
        }

        /// <inheritdoc/>
        public void Remove(TEntity entity) => _dbSet.Value.Remove(entity);

        /// <inheritdoc/>
        public void RemoveRange(IEnumerable<TEntity> entities) => _dbSet.Value.RemoveRange(entities);

        /// <inheritdoc/>
        public TEntity Update(TEntity entity)
        {
            var added = _dbSet.Value.Update(entity);
            return added.Entity;
        }

        private IQueryable<TEntity> FilterAndOrder<TField>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, TField>> orderByKeySelector = null, bool orderAscending = true, Func<IIncludable<TEntity>, IIncludable> includes = null)
        {
            var query = _dbSet.Value.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderByKeySelector != null)
            {
                if (orderAscending)
                {
                    query = query.OrderBy(orderByKeySelector);
                }
                else
                {
                    query = query.OrderByDescending(orderByKeySelector);
                }
            }

            if (includes != null)
            {
                query = query.IncludeMultiple(includes);
            }

            return query;
        }
    }
}
