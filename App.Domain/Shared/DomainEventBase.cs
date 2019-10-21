using System;

namespace App.Domain.Shared
{
    public class DomainEventBase : IDomainEvent
    {
        public DomainEventBase() => OccurredOn = DateTimeOffset.Now;

        public DateTimeOffset OccurredOn { get; }
    }
}
