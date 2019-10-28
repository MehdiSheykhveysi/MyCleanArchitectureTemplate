using App.ApplicationService.ToDoItems.Dtos;
using App.ApplicationService.ToDoItems.Specifications;
using MediatR;

namespace App.ApplicationService.ToDoItems.UseCases.Queries
{
    public class ToDoItemGetByIdQuery : IRequest<ToDoItemQueryDto>
    {
        public ToDoItemGetByIdSpecification ToDoItemGetByIdSpecification { get; }

        public ToDoItemGetByIdQuery(ToDoItemGetByIdSpecification toDoItemGetByIdSpecification)
        {
            ToDoItemGetByIdSpecification = toDoItemGetByIdSpecification;
        }
    }
}
