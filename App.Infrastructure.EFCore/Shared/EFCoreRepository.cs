using App.ApplicationService.Shaared.Attributes;
using App.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace App.Infrastructure.Shared
{
    [ServiceMark]
    public class EFCoreRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : Entity<TKey> where TKey : IEquatable<TKey>
    {
        public EFCoreRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            entities = _dbContext.Set<TEntity>();
        }

        private readonly AppDbContext _dbContext;
        private readonly DbSet<TEntity> entities;

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) =>
            await entities
                       .Where(predicate)
                       .CountAsync(cancellationToken)
                       .ConfigureAwait(false);

        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            entities.Remove(entity);
            await SaveChangesAsync(cancellationToken);

        }

        public async Task<IEnumerable<ProjectionType>> GetPagedAsync<ProjectionType, orderTkey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, ProjectionType>> selector, Expression<Func<TEntity, orderTkey>> orderBySelector, int pageSize, int takeItem, int currentIndex, CancellationToken cancellationToken) where ProjectionType : class
        {
            return await entities.Where(predicate)
                                        .OrderBy(orderBySelector)
                                        .Skip(pageSize)
                                        .Take(takeItem)
                                        .Select(selector)
                                .ToListAsync(cancellationToken)
                                .ConfigureAwait(false);
        }

        public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await entities.AddAsync(entity, cancellationToken);
            await SaveChangesAsync(cancellationToken);
        }

        public async Task InsertOrUpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            if (await entities.ContainsAsync(entity, cancellationToken))
                await UpdateAsync(entity, cancellationToken);
            else
                await InsertAsync(entity, cancellationToken);
        }

        public async Task LoadCollectionAsync<TProperty>(TEntity entity, System.Linq.Expressions.Expression<Func<TEntity, IEnumerable<TProperty>>> propertyExpression, CancellationToken cancellationToken) where TProperty : class
        {
            this.Attach(entity);
            var collection = _dbContext.Entry(entity).Collection(propertyExpression);
            if (!collection.IsLoaded)
                await collection.LoadAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task LoadRefrenceAsync<TProperty>(TEntity entity, System.Linq.Expressions.Expression<Func<TEntity, TProperty>> propertyExpression, CancellationToken cancellationToken) where TProperty : class
        {
            this.Attach(entity);
            var refrence = _dbContext.Entry(entity).Reference(propertyExpression);
            if (!refrence.IsLoaded)
                await refrence.LoadAsync(cancellationToken).ConfigureAwait(true);
        }

        public async Task<TEntity> SingleItemAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await entities
                             .Where(predicate)
                             .SingleOrDefaultAsync(cancellationToken)
                             .ConfigureAwait(false);
        }

        public async Task<TEntity> FirstItemAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await entities
                            .Where(predicate)
                            .FirstOrDefaultAsync(cancellationToken)
                            .ConfigureAwait(false);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            entities.Update(entity);
            if (_dbContext.Entry(entity).State == EntityState.Detached)
                Attach(entity);
            await SaveChangesAsync(cancellationToken);
        }

        private async Task SaveChangesAsync(CancellationToken cancellationToken) =>
            await _dbContext.SaveChangesAsync(cancellationToken);

        private void Attach(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
                _dbContext.Attach(entity);
        }

        public async Task<IEnumerable<ProjectionType>> GetAllAsync<ProjectionType>(Expression<Func<TEntity, ProjectionType>> selector, CancellationToken cancellationToken)
        {
            return await entities.Select(selector).ToListAsync(cancellationToken);
        }
    }
}
