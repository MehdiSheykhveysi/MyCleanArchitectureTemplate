using MediatR;
using System;

namespace App.Domain.Shared
{
    internal interface IDomainEvent : INotification
    {
        DateTimeOffset OccurredOn { get; }
    }
}
