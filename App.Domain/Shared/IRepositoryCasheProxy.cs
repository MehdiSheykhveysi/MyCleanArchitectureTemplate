using System;

namespace App.Domain.Shared
{
    public interface IRepositoryCacheProxy<TEntity, Tkey> : IRepository<TEntity, Tkey>
        where TEntity : Entity<Tkey>
        where Tkey : IEquatable<Tkey>
    {
    }
}