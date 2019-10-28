using App.ApplicationService.ToDoItems.Dtos;
using App.Domain.Entities;
using App.Domain.Shared;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App.ApplicationService.ToDoItems.UseCases.Queries
{
    public class ToDoItemPagedQueryHandler : IRequestHandler<ToDoItemPagedQuery, PaginatedList<ToDoItemQueryDto>>
    {
        private readonly IRepository<ToDoItem, int> _repository;

        public ToDoItemPagedQueryHandler(IRepository<ToDoItem, int> repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedList<ToDoItemQueryDto>> Handle(ToDoItemPagedQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<ToDoItemQueryDto> items = await _repository.GetPagedAsync(request._allItemSpecification.ToExpression(), request._projectionSpecification.ToExpression(), request._orderByIdSpecification.ToExpression(), request.PageSize, request.TakeItem, request.CurrentIndex, cancellationToken);

            return new PaginatedList<ToDoItemQueryDto>(items, request.TakeItem, request.CurrentIndex, request.PageSize);
        }
    }
}