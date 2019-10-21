using App.ApplicationService.ToDoItems.Dtos;
using App.ApplicationService.ToDoItems.Specifications;
using MediatR;

namespace App.ApplicationService.ToDoItems.UseCases.Queries
{
    public class ToDoItemQuery : IRequest<PaginatedList<ToDoItemQueryDto>>
    {
        public ToDoAllItemSpecification _allItemSpecification { get; }
        public ProjectionSpecification _projectionSpecification { get; }
        public OrderByIdSpecification _orderByIdSpecification { get; }
        public int PageSize { get; }
        public int TakeItem { get; }
        public int CurrentIndex { get; }

        public ToDoItemQuery(ToDoAllItemSpecification allItemSpecification, ProjectionSpecification projectionSpecification, OrderByIdSpecification orderByIdSpecification, int pageSize, int takeItem, int currentItndex)
        {
            _allItemSpecification = allItemSpecification;
            _projectionSpecification = projectionSpecification;
            _orderByIdSpecification = orderByIdSpecification;
            PageSize = pageSize;
            TakeItem = takeItem;
            CurrentIndex = currentItndex;
        }
    }
}
