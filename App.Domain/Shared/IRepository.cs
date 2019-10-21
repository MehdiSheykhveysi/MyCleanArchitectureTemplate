using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace App.Domain.Shared
{
    public interface IRepository<TEntity, TKey> where TEntity : Entity<TKey> where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<ProjectionType>> GetAllAsync<ProjectionType>(Expression<Func<TEntity, ProjectionType>> selector, CancellationToken cancellationToken);

        Task<IEnumerable<ProjectionType>> GetPagedAsync<ProjectionType, orderTkey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, ProjectionType>> selector, Expression<Func<TEntity, orderTkey>> orderBySelector, int pageSize, int takeItem, int currentIndex, CancellationToken cancellationToken) where ProjectionType : class;

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        Task<TEntity> SingleItemAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        Task<TEntity> FirstItemAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        Task InsertAsync(TEntity entity, CancellationToken cancellationToken);

        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

        Task InsertOrUpdateAsync(TEntity entity, CancellationToken cancellationToken);

        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);

        Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> propertyExpression, CancellationToken cancellationToken) where TProperty : class;

        Task LoadRefrenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> propertyExpression, CancellationToken cancellationToken) where TProperty : class;
    }
}
