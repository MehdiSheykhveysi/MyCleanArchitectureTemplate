using App.Domain.Shared;

namespace App.ApplicationService.ToDoItems.Events
{
    public class CreateToDoItemEvent : DomainEventBase
    {
        public CreateToDoItemEvent(string description)
        {
            Description = description;
        }

        public string Description { get; set; }
    }
}
