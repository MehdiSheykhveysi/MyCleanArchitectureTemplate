using App.ApplicationService.ToDoItems.Dtos;
using MediatR;

namespace App.ApplicationService.ToDoItems.UseCases.Commands.CreateNewItem
{
    public class EditToDoItemCommand : IRequest<EditToDoItem>
    {
        public EditToDoItem EditToDoItem { get; set; }

        public EditToDoItemCommand(EditToDoItem editToDoItem)
        {
            EditToDoItem = editToDoItem;
        }
    }
}