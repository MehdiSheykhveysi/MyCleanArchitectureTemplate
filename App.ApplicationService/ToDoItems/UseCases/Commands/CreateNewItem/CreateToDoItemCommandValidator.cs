using FluentValidation;

namespace App.ApplicationService.ToDoItems.UseCases.Commands.CreateNewItem
{
    public class CreateToDoItemCommandValidator : AbstractValidator<CreateToDoItemCommand>
    {
        public CreateToDoItemCommandValidator()
        {
            RuleFor(c => c.Description).NotEmpty().WithMessage("Description Property Cann't Be Empty");
        }
    }
}