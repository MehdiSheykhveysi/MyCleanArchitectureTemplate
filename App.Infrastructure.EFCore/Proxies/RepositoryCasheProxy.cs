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
    public class RepositoryCasheProxy<TEntity, Tkey> : IRepositoryCasheProxy<TEntity, Tkey> where TEntity : Entity<Tkey> where Tkey : IEquatable<Tkey>
    {

        public RepositoryCasheProxy(IRepository<TEntity, Tkey> repository, ICacheAdapter cacheAdapter)
        {
            _repository = repository;
            CacheAdapter = cacheAdapter;
        }

        private readonly IRepository<TEntity, Tkey> _repository;

        public ICacheAdapter CacheAdapter { get; set; }


        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return _repository.CountAsync(predicate, cancellationToken);
        }

        public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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
            await _repository.InsertAsync(entity, cancellationToken);
        }

        public Task InsertOrUpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            return _repository.InsertOrUpdateAsync(entity, cancellationToken);
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

        public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            return _repository.UpdateAsync(entity, cancellationToken);
        }

        public async Task<IEnumerable<ProjectionType>> GetAllAsync<ProjectionType>(Expression<Func<TEntity, ProjectionType>> selector, CancellationToken cancellationToken)
        {
            IEnumerable<ProjectionType> result = new List<ProjectionType>();
            if (selector.GetRightConstant() is TEntity entity)
            {
                string key = $"Collection{entity.Id}";
                if (CacheAdapter.Exist(key))
                    return CacheAdapter.Get<IEnumerable<ProjectionType>>(key);
                else
                {
                    result = await _repository.GetAllAsync(selector, cancellationToken);
                    CacheAdapter.Add(key, result);
                }
            }
            return result;
        }
    }
}
