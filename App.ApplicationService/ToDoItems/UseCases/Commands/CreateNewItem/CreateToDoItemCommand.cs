using App.ApplicationService.ToDoItems.Dtos;
using MediatR;

namespace App.ApplicationService.ToDoItems.UseCases.Commands.CreateNewItem
{
    public class CreateToDoItemCommand:IRequest<CreateIItemDto>
    {
        public string Description { get; set; }
    }
}
