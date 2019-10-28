using App.ApplicationService.ToDoItems.Dtos;
using App.Domain.Entities;
using App.Domain.Shared;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App.ApplicationService.ToDoItems.UseCases.Queries
{
    public class ToDoAllItemQueryHandler : IRequestHandler<ToDoAllItemQuery, IEnumerable<ToDoItemQueryDto>>
    {
        private readonly IRepositoryCasheProxy<ToDoItem, int> _repository;

        public ToDoAllItemQueryHandler(IRepositoryCasheProxy<ToDoItem, int> repository)
        {
            _repository = repository;
        }

        //public Task<IEnumerable<ToDoItemQueryDto>> Handle(ToDoAllItemQuery request, CancellationToken cancellationToken)
        //{
        //    return _repository.GetAllAsync(request._projectionSpecification.ToExpression(), cancellationToken);
        //}

        public Task<IEnumerable<ToDoItemQueryDto>> Handle(ToDoAllItemQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetAllAsync(request._projectionSpecification.ToExpression(), cancellationToken);
        }
    }
}
