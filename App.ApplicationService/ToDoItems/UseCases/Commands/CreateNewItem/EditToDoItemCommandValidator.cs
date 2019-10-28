using FluentValidation;
namespace App.ApplicationService.ToDoItems.UseCases.Commands.CreateNewItem
{
    public class EditToDoItemCommandValidator : AbstractValidator<EditToDoItemCommand>
    {
        public EditToDoItemCommandValidator()
        {
            RuleFor(c => c.EditToDoItem.Description).NotEmpty().WithMessage("we need a 'Description' property which cannot be null or empty.fill that please");
            RuleFor(c => c.EditToDoItem.Id).NotEmpty().NotEqual(0).WithMessage("ToDoItem Id property can't be empty or zero(0)");
        }
    }
}