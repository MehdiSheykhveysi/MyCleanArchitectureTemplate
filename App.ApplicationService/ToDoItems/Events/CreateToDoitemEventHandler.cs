using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.ApplicationService.ToDoItems.Events
{
    public class CreateToDoitemEventHandler : INotificationHandler<CreateToDoItemEvent>
    {
        private readonly ILogger<CreateToDoItemEvent> _logger;

        public CreateToDoitemEventHandler(ILogger<CreateToDoItemEvent> logger)
        {
            _logger = logger;
        }

        public Task Handle(CreateToDoItemEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"A new TDoItem object Created some information about  is '{notification.Description}' at {notification.OccurredOn}");

            return Task.CompletedTask;
        }
    }
}
