using App.ApplicationService.ToDoItems.Dtos;
using App.ApplicationService.ToDoItems.Specifications;
using MediatR;
using System.Collections.Generic;

namespace App.ApplicationService.ToDoItems.UseCases.Queries
{
    public class ToDoAllItemQuery : IRequest<IEnumerable<ToDoItemQueryDto>>
    {
        public ProjectionSpecification _projectionSpecification { get; }

        public ToDoAllItemQuery(ProjectionSpecification projectionSpecification) => _projectionSpecification = projectionSpecification;
    }
}
