using System;

namespace App.Domain.Shared
{
    public abstract class Entity<TKey> : IEntity
    {
        public TKey Id { get; set; }
    }

    public abstract class IntEntity : Entity<int>
    {
    }
}
