using App.ApplicationService.Shaared.Attributes;
using App.Domain.Shared;
using App.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace App.Infrastructure.Proxies
{
    [ServiceMark]
    public class RepositoryCasheProxy<TEntity, Tkey> : IRepositoryCacheProxy<TEntity, Tkey> where TEntity : Entity<Tkey> where Tkey : IEquatable<Tkey>
    {

        public RepositoryCasheProxy(IRepository<TEntity, Tkey> repository, ICacheAdapter cacheAdapter)
        {
            _repository = repository;
            CacheAdapter = cacheAdapter;
        }

        private readonly IRepository<TEntity, Tkey> _repository;

        private readonly string SingleObjectCacheKey = $"Entity-Cached ";

        private readonly ICacheAdapter CacheAdapter;

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return _repository.CountAsync(predicate, cancellationToken);
        }

        public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            string key = string.Concat(SingleObjectCacheKey, entity.Id);

            CacheAdapter.Remove(key);

            return _repository.DeleteAsync(entity, cancellationToken);
        }

        public Task<TEntity> FirstItemAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return _repository.FirstItemAsync(predicate, cancellationToken);
        }

        public Task<IEnumerable<ProjectionType>> GetPagedAsync<ProjectionType, orderTkey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, ProjectionType>> selector, Expression<Func<TEntity, orderTkey>> orderBySelector, int pageSize, int takeItem, int currentIndex, CancellationToken cancellationToken) where ProjectionType : class
        {
            return _repository.GetPagedAsync(predicate, selector, orderBySelector, pageSize, takeItem, currentIndex, cancellationToken);
        }

        public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken)
        {
            string key = string.Concat(SingleObjectCacheKey, entity.Id);

            await _repository.InsertAsync(entity, cancellationToken);

            CacheAdapter.Add(key, entity);
        }

        public async Task InsertOrUpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            string key = string.Concat(SingleObjectCacheKey, entity.Id);

            await _repository.InsertOrUpdateAsync(entity, cancellationToken);

            CacheAdapter.AddOrUpdate(key, entity);
        }

        public Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> propertyExpression, CancellationToken cancellationToken) where TProperty : class
        {
            return _repository.LoadCollectionAsync(entity, propertyExpression, cancellationToken);
        }

        public Task LoadRefrenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> propertyExpression, CancellationToken cancellationToken) where TProperty : class
        {
            return LoadRefrenceAsync(entity, propertyExpression, cancellationToken);
        }

        public Task<TEntity> SingleItemAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return _repository.SingleItemAsync(predicate, cancellationToken);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            string key = string.Concat(SingleObjectCacheKey, entity.Id);
           
            await _repository.UpdateAsync(entity, cancellationToken);

            CacheAdapter.Update(key, entity);
        }

        public async Task<IEnumerable<ProjectionType>> GetAllAsync<ProjectionType>(Expression<Func<TEntity, ProjectionType>> selector, CancellationToken cancellationToken)
        {
            IEnumerable<ProjectionType> result = new List<ProjectionType>();

            string CacheKey = $"CachedRepository-{typeof(TEntity).Name}";

            if (CacheAdapter.Exist(CacheKey))
                return CacheAdapter.Get<IEnumerable<ProjectionType>>(CacheKey);
            else
            {
                result = await _repository.GetAllAsync(selector, cancellationToken);
                CacheAdapter.Add(CacheKey, result);
            }
            return result;
        }
    }
}
