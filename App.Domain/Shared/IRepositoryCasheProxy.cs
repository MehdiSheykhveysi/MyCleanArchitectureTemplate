using System;

namespace App.Domain.Shared
{
    public interface IRepositoryCasheProxy<TEntity, Tkey> : IRepository<TEntity, Tkey>
        where TEntity : Entity<Tkey>
        where Tkey : IEquatable<Tkey>
    {
       ICacheAdapter CacheAdapter { get; set; }
    }
}