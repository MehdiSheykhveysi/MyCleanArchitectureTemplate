using MediatR;

namespace App.ApplicationService.ToDoItems.UseCases.Commands.CreateNewItem
{
    public class DeleteToDoItemCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteToDoItemCommand(int id)
        {
            Id = id;
        }
    }
}
